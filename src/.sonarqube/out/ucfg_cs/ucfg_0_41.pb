
EMix.Common.Helper.CommonHelper.SaveFileBase64(string, string, string)^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(	folderfilename	strBase64"0*
0*
1
2*�
2rp
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�. �(
%0"string[]�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%1"
__arraySet*

%0*


folder�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%2"
__arraySet*

%0*


filename�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�" �(
%3"4Mix.Common.Helper.CommonHelper.GetFullPath(string[])*"" 
Mix.Common.Helper.CommonHelper*

%0|
z
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(

fullPath"__id*

%3�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�6 �(L
%4"string.IndexOf(char)*

	strBase64*
""�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�" �(Q
%5"string.Substring(int)*

	strBase64*
""|
z
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(Q

fileData"__id*

%5�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(A
%6"'System.Convert.FromBase64String(string)*"
System.Convert*


fileDatay
w
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(A
bytes"__id*

%6�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(-
%7""System.IO.Directory.Exists(string)*"
System.IO.Directory*


folder*
3
4*�
3�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(5
%8"+System.IO.Directory.CreateDirectory(string)*"
System.IO.Directory*


folder*
4*�
4�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �()
%9"System.IO.File.Exists(string)*"
System.IO.File*


fullPath*
5
6*�
5�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �()
%10"System.IO.File.Delete(string)*"
System.IO.File*


fullPath*
6*�
6�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�9 �(H
%11"__id*"* 
System.IO.FileMode"
Create}
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�  �(I
%12"System.IO.FileStream�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�$ �(.
%13";System.IO.FileStream.FileStream(string, System.IO.FileMode)*

%12*


fullPath*

%11w
u
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(I
fs"__id*

%12�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�! �(5
%14"System.IO.BinaryWriter�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�% �(1
%15"5System.IO.BinaryWriter.BinaryWriter(System.IO.Stream)*

%14*

fsv
t
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(5
w"__id*

%14*
7
8*�
7�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �("
%16"$System.IO.BinaryWriter.Write(byte[])*

w*	

bytes*
9
8*�
8�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%17"System.IO.Stream.Close()*

fs�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%18"System.IO.BinaryWriter.Close()*

w*
10*	
9*
11*n
11"h
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
""*
12*
1
10*m
1"h
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
""*
10"
""