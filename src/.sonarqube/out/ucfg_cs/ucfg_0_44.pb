
?Mix.Common.Helper.CommonHelper.WriteBytesToFile(string, string)^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(	fullPath	strBase64"0*�
0�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�2 �(H
%0"string.IndexOf(char)*

	strBase64*
""�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(M
%1"string.Substring(int)*

	strBase64*
""|
z
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(M

fileData"__id*

%1�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(=
%2"'System.Convert.FromBase64String(string)*"
System.Convert*


fileDatay
w
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(=
bytes"__id*

%2�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(%
%3"System.IO.File.Exists(string)*"
System.IO.File*


fullPath*
1
2*�
1�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(%
%4"System.IO.File.Delete(string)*"
System.IO.File*


fullPath*
2*�
2�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�5 �(D
%5"__id*"* 
System.IO.FileMode"
Create~|
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(E
%6"System.IO.FileStream�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�  �(*
%7";System.IO.FileStream.FileStream(string, System.IO.FileMode)*

%6*


fullPath*

%5v
t
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(E
fs"__id*

%6�~
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(1
%8"System.IO.BinaryWriter�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs�! �(-
%9"5System.IO.BinaryWriter.BinaryWriter(System.IO.Stream)*

%8*

fsu
s
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(1
w"__id*

%8*
3
4*�
3�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%10"$System.IO.BinaryWriter.Write(byte[])*

w*	

bytes*
5
4*�
4�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%11"System.IO.Stream.Close()*

fs�
�
^
RC:\_workspace\github\mixcore\mix.heart\src\Mix.Heart\Common\Helper\CommonHelper.cs� �(
%12"System.IO.BinaryWriter.Close()*

w*
6*
5*
6*
6"
""