using System;

namespace HighSpotJson
{
    class Program
    {
        static void Main(string[] args)
        {

            /*Validate Inputs 
            1) Mixtape Jason file
            2) changes file 
            //to do output file path - currenly it will save at same place as execuatable file
            3) output directory optinal 
            */
            HighSpotJson.Helper.SerializeHelper sHelper = new Helper.SerializeHelper();
            Model.MixtapeDatamodel inputMixtapeModel = sHelper.GetModel(Helper.Constants.InputTypeEnum.MixTape);
            Model.MixtapeDatamodel changeMixtapeModel = sHelper.GetModel(Helper.Constants.InputTypeEnum.ChangeMixTape);

            if (inputMixtapeModel == null || changeMixtapeModel == null)
            {
                Console.WriteLine("Error in input file(s) aborting operation");
                Console.WriteLine("Program terminated.");
                Console.ReadLine();
                return;
            }

            BatchProcess batch = new BatchProcess(inputMixtapeModel, changeMixtapeModel);
            Model.MixtapeDatamodel OutputMixtapeModel = batch.ProcessOutputJson();
            sHelper.SaveModel(OutputMixtapeModel);
            Console.WriteLine("Program completed.");
            Console.ReadLine();
        }
    }
}
