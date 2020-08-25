# OPEN SUBTITLES HANDLER
A library that implement most of OpenSubtitles.org XML-RPC methods. 

### GetHash library
http://trac.opensubtitles.org/projects/opensubtitles/attachment/wiki/HashSourceCodes/GetHash.dll

### Subtitles service powered by OpenSubtitles.org 'http://www.opensubtitles.org'

## Introducrion:
OpenSubtitlesHandler is a library that implement all OpenSubtitles.org XML-RPC methods.
This library uses internal XML-RPC generator that can generate/decode XML-RPC requests. 
All OpenSubtitles methods tested and work except the methods that refered not to work (see implemented methods table below).

## Features:

- All OpenSubtitles.org service XML-RPC methods implemented.
- Internal XML-RPC generator and decoder.
- A full test gui test all OS methods and XML-RPC generator/decoder.

## System requirements:
- .NET Framework 4. (Note: you can change the taget framework any time using the source, it can work with framework 2 !)
- OS: Windows XP/7 and 8.

## How to use:

### OpenSubtitlesHandler:

- First of all, add OpenSubtitlesHandler.dll to your project. (in solution explorer, right click on references and chose
  Add reference, browse for OpenSubtitlesHandler.dll).

- This library includes OpenSubtitles.cs class under OpenSubtitlesHandler namespace, it holds all OS methods as 'static' members. 
You should call OpenSubtitles.SetUserAgent(useragent); first. 
By default it set to "OS Test User Agent" (see 'http://trac.opensubtitles.org/projects/opensubtitles/wiki/DevReadFirst').

- Then no need to do anything but to call OS methods !
Example: to log in:
~~~
IMethodResponse result = OpenSubtitles.LogIn(username, password, useragent);
~~~
The result will be MethodResponseError if something went wrong like connection error. If it success, the response will MethodResponseLogIn. This class includes the decoded server response, not XML-RPC ! it include 2 properties: Status and Seconds.

- All other method work the same:
~~~
IMethodResponse result = OpenSubtitles.InsertMovie(movieName, movieyear);
~~~
If the call success the response will be MethodResponseInsertMovie which include these properties: Seconds, Status and ID !

- All you have to do is to deal with responses. Each method has it's own response when the call success and all share the MethodResponseError if something went wrong like connection error.

- Full example:
~~~
private void DownloadSubtitles(int[] downloadsList)
{
  Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OpesSubtitlesHandlerTest\\");
  IMethodResponse result = OpenSubtitles.DownloadSubtitles(downloadsList);// we send array holds sub ids !
  if (result is MethodResponseSubtitleDownload)
  {
     MethodResponseSubtitleDownload response = (MethodResponseSubtitleDownload)result; 
     foreach (SubtitleDownloadResult res in response.Results)
     {
      byte[] data = Convert.FromBase64String(res.Data);//Not decompressed and decoded yet ...
      byte[] target = Utilities.Decompress(new MemoryStream(data));
      // now save the subtitle file
      string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OpesSubtitlesHandlerTest\\";
      // Get file name
      // Write code to get sub name from SearchSubtitles method ...
      fileName += "Untitled.srt";// use this for now ...
      // write data
      Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
      stream.Write(target, 0, target.Length);
      stream.Close();

      Console.WriteLine("Done ! file saved to:");
      Console.WriteLine(fileName);
     }
  }
}
~~~
This example downloads subtitle(s) from OS server and save it at documents folder.

## OpenSubtitlesHandlerTest:

This project is a gui that test all OS methods of OpenSubtitles.cs.

- Test LogIn:
1 Enter useragent, username and password in the fields at Session handling section.
2 Click LogIn button.
3 The console (black box) will write the results.

- Test SearchSubtitles:
1 In Search and download section, click 'Open Movie' link.
2 Browse for movie then click open. This will calculate Hash and size of the movie.
3 Change the language id field if like (e.g. eng,ara,ger ..)
4 Click SearchSubtitles button.
5 If the search success, the Results list get filled with results. Select one of them.
6 The Object view will show you the result content !

- Test DownloadSubtitles:
1 Complete the SearchSubtitles test above first.
2 Select a result from the results list.
3 Click DownloadSubtitles. This will send the id of selected subtitle result.
4 The console will tell if the call success and where the file saved.

## Implementet OpenSubtitles.org service XML-RPC methods.

Legends:
- OK: this method is fully implemented, tested and works fine.
- TODO: this method is in the plan to be added.
- NOT TESTED: this method added and expected to work fine but never tested.
- NOT WORK (x): this method added but not work. x= Description of the error.

--------------------------------------------
Method name              | Status
-------------------------|------------------
LogIn                    | OK
LogOut                   | OK
NoOperation              | OK
SearchSubtitles          | OK
DownloadSubtitles        | OK
SearchToMail             | OK
TryUploadSubtitles       | OK
UploadSubtitles          | OK
SearchMoviesOnIMDB       | OK
GetIMDBMovieDetails      | OK
InsertMovie              | OK
InsertMovieHash          | OK
ServerInfo               | OK
ReportWrongMovieHash     | OK
ReportWrongImdbMovie     | OK
SubtitlesVote            | OK
AddComment               | OK
AddRequest               | OK
GetComments              | OK
GetSubLanguages          | OK
DetectLanguage           | OK
GetAvailableTranslations | OK
GetTranslation           | NOT WORK (Returns status of error 410 'Other or unknown error')
AutoUpdate               | NOT WORK (Returns status: 'parse error. not well formed')
CheckMovieHash           | OK
CheckMovieHash2          | OK
CheckSubHash             | OK
--------------------------------------------

Note: if a method not work or there's missing methods, please report them to alahadid@hotmial.com.


## Credits and resources:
- Author, developer and programmer: Ala Ibrahim Hadid. (mailto:alahadid@hotmail.com)
- XML-RPC generator: created using information from http://xmlrpc.scripting.com/spec.html
- OS methods: created using information from http://trac.opensubtitles.org/projects/opensubtitles/wiki/XMLRPC, http://trac.opensubtitles.org/projects/opensubtitles/wiki/XmlRpcIntro
