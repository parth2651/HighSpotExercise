# HighSpotExercise
HighSpot Sample Exercise by Parth Patel

* This high level solution developed in .net core v3.1 

## Usage 
* Input:
  * Mixtape.json (input jason file)
  * ChangeMixtape.json (change file almost same file with one extra property on input)
  * Default path is set to current directory - and default files name is added
* Output: 
  * output.json file will be saved at the same directory.
  
~~~
dotnet run
~~~


## Assumption

* Use id as string as it could be GUID or custom id with huge users/songs/etc. 
* Assuming Songs are exists and for any check all validation is just based on ID (if id exists then only songs exists)
* Input files are not validated (just created function - Files) but should be validated based on rules. Currently assumed all songs and plalist will have ID.
* No validation if uses exits or not.
* if any of the input files are not valid/blank do not process further.
* if one song/playlist did not found continue with other changs with.
* using out of the box JSON parser - can use some third party parser in case of very large files and need to process distributed.


## change file 

change file will be replica of MixTape.json with additional action property to all the object. This will be easy to maintain model and validation for the inputs.
below are the action for Playlist (This can be extened to Song and Users).

Same can have multiple operation so it will process sequencially but can be added order property in change file if used with multiple thread/process.


## Scalability

There are multiple facads to be consider to scale this operatons (No infrastructure consideration - assuming we have unlimited resource) 
* Options 
  * MicroService
    * Different service to read files, save file and process batch
    * Seperate processe for songs, playlist and users
    * handle all three seperately and when requested generate pack
  * LargeFile/Data: find a way to chunck the file to imperove process, if possible generate JSON with DB.
    * Large file can be diveded with object and create seperate file users/ playlist/ songs
    * Alternatively can be process just by the single users which reduce file size and easy to distrubute either thread or machiens.
    * Song data should be seperate service or one time load and keep in cache to validate existance if not modified by users.
  * Output file
    * If possible DB will be better choice for writing and maintaining consistancy and generate output from DB if file requirement is must.
    * Implement lock while generating output file while implement distribution/ multithread. 
  * Log/Error
    * along with loggin, implement impacted objects/elements in a seperate file/table (i.e. song not exist but added in playlist) to process further.
    * this will help to process non-errored faster and only implected users will be handled seperately to reduce impact on customers. 
  * Validation
    * If posisble validation should enforced for data consistancy. 
  

