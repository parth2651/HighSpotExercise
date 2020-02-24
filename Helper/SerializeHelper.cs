using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace HighSpotJson.Helper
{
    class SerializeHelper
    {
        /// <summary>
        /// get model from file
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="path">full path with file name;
        /// optional with default directory current application directory with MixTape.json filename</param>
        /// <returns></returns>
        public HighSpotJson.Model.MixtapeDatamodel GetModel(Helper.Constants.InputTypeEnum inputType, string path = "")
        {
            HighSpotJson.Model.MixtapeDatamodel jsonModel = null;

            try
            {

                //To Do validate Path with file name if exists
                if (string.IsNullOrEmpty(path))
                {
                    if (inputType == Constants.InputTypeEnum.MixTape)
                        path = Directory.GetCurrentDirectory() + "\\MixTape.json";
                    else if (inputType == Constants.InputTypeEnum.ChangeMixTape)
                        path = Directory.GetCurrentDirectory() + "\\ChangeMixTape.json";
                }
                var jsonString = File.ReadAllText(path);
                jsonModel = JsonSerializer.Deserialize<HighSpotJson.Model.MixtapeDatamodel>(jsonString);
                //To Do Validate inbound Model for any validation
                jsonModel = ValidateModel(jsonModel, inputType);


            }
            catch (Exception ex)
            {
                Helper.LogHelper.LogInformation("Error while reading model");
                Helper.LogHelper.LogError(ex);
            }
            return jsonModel;

        }
        /// <summary>
        /// Save Model to Json file
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="path">full path with file name;
        /// optional with default directory as current application directory with output.json filename</param>
        public void SaveModel(HighSpotJson.Model.MixtapeDatamodel Model, string path = "")
        {
            try
            {

                //To Do validate Path with file name if exists
                if (string.IsNullOrEmpty(path))
                {
                    path = Directory.GetCurrentDirectory() + "\\output.json";
                }

                var jsonString = JsonSerializer.Serialize(Model);
                using (StreamWriter outputFile = new StreamWriter(path, false))
                {
                    outputFile.WriteLine(jsonString);
                }
            }
            catch (Exception ex)
            {
                Helper.LogHelper.LogInformation("Error while saving model");
                Helper.LogHelper.LogError(ex);
            }
        }
        /// <summary>
        /// Validate model with file business rules and consistancy 
        /// </summary>
        /// <param name="jsonModel"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public HighSpotJson.Model.MixtapeDatamodel ValidateModel(HighSpotJson.Model.MixtapeDatamodel jsonModel,
        Constants.InputTypeEnum inputType)
        {
            try
            {
                switch (inputType)
                {
                    case Constants.InputTypeEnum.MixTape:
                        jsonModel = ValidateMixTapeModal(jsonModel);
                        break;
                    case Constants.InputTypeEnum.ChangeMixTape:
                        jsonModel = ValidateChangeMixTapeModel(jsonModel);
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.LogHelper.LogInformation("Error while validating model");
                Helper.LogHelper.LogError(ex);
                return null; //returning null while error so consitant in handling 
            }
            return jsonModel;
        }


        private HighSpotJson.Model.MixtapeDatamodel ValidateMixTapeModal(HighSpotJson.Model.MixtapeDatamodel jsonModel)
        {
            //to do validate condition like empty list
            //atelast user exits or song exists or duplicate check etc
            //check duplicate ID
            //any business rules    
            if (jsonModel == null)
                Console.WriteLine(Helper.Constants.Messages.ValidateMixTape);
            return jsonModel;
        }
        private HighSpotJson.Model.MixtapeDatamodel ValidateChangeMixTapeModel(HighSpotJson.Model.MixtapeDatamodel jsonModel)
        {
            //to do validate condition like empty list
            //atelast user exits or song exists or duplicate check etc
            //any business rules    
            if (jsonModel == null)
                Console.WriteLine(Helper.Constants.Messages.ValidateChangeMixTape);
            return jsonModel;
        }



    }



}