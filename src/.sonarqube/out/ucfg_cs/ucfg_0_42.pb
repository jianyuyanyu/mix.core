
[Mix.Common.Helper.CommonHelper.UploadFileAsync(string, Microsoft.AspNetCore.Http.IFormFile)^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(	fullPathfile"0*
0*
1
2*�
2�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(/
%0""System.IO.Directory.Exists(string)*"
System.IO.Directory*


fullPath*
3
4*�
3�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(7
%1"+System.IO.Directory.CreateDirectory(string)*"
System.IO.Directory*


fullPath*
4*�
4�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �( 
%2""object.operator !=(object, object)*
"
object*

file*
""*
5
6*�
5�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(&
%3"System.Guid.NewGuid()*"
System.Guid�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(4
%4"System.Guid.ToString(string)*

%3*
""�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(%
%5"0Microsoft.AspNetCore.Http.IFormFile.FileName.get*

file�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(0
%6"string.Split(params char[])*

%5*
""�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(7
%7"USystem.Linq.Enumerable.Last<TSource>(System.Collections.Generic.IEnumerable<TSource>)*"
System.Linq.Enumerable*

%6�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�& �(8
%8"%string.Format(string, object, object)*
"
string*
""*

%4*

%7|
z
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(8

fileName"__id*

%8�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�; �([
%9"&System.IO.Path.Combine(string, string)*"
System.IO.Path*


fullPath*


fileName�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�] �(l
%10"__id*"* 
System.IO.FileMode"
Create�
�
_
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�n �(�
%11"__id*'*%
System.IO.FileAccess"
	ReadWrite�~
_
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�, �(�
%12"System.IO.FileStream�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�0 �(:
%13"QSystem.IO.FileStream.FileStream(string, System.IO.FileMode, System.IO.FileAccess)*

%12*

%9*

%10*

%11�
~
_
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(�

fileStream"__id*

%12*
7*�
7�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(:
%14"eMicrosoft.AspNetCore.Http.IFormFile.CopyToAsync(System.IO.Stream, System.Threading.CancellationToken)*

file*


fileStream*
""�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(P
%15"0System.Threading.Tasks.Task.ConfigureAwait(bool)*

%14*
"""n
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �((


fileName*
8*
9*�
6�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �('
%16"__id**
string"
Empty"i
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �((

%16*
9*
1
10*�
1�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(#
%17"__id**
string"
Empty"i
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �($

%17*
10"
""