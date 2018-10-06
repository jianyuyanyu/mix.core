�R
OC:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\App_Start\Startup.Auth.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
{ 
public 

partial 
class 
Startup  
{ 
	protected 
void 
ConfigIdentity %
(% &
IServiceCollection& 8
services9 A
,A B
IConfigurationC Q
ConfigurationR _
,_ `
stringa g
connectionNameh v
)v w
{ 	
services 
. 
AddDbContext !
<! "
MixDbContext" .
>. /
(/ 0
)0 1
;1 2
PasswordOptions 
pOpt  
=! "
new# &
PasswordOptions' 6
(6 7
)7 8
{ 
RequireDigit   
=   
false   $
,  $ %
RequiredLength!! 
=!!  
$num!!! "
,!!" #
RequireLowercase""  
=""! "
false""# (
,""( )"
RequireNonAlphanumeric## &
=##' (
false##) .
,##. /
RequireUppercase$$  
=$$! "
false$$# (
}%% 
;%% 
services'' 
.'' 
AddIdentity''  
<''  !
ApplicationUser''! 0
,''0 1
IdentityRole''2 >
>''> ?
(''? @
options''@ G
=>''H J
{(( 
options)) 
.)) 
Password))  
=))! "
pOpt))# '
;))' (
}** 
)** 
.++ $
AddEntityFrameworkStores++ )
<++) *
MixDbContext++* 6
>++6 7
(++7 8
)++8 9
.,, $
AddDefaultTokenProviders,, )
(,,) *
),,* +
.-- 
AddUserManager-- 
<--  
UserManager--  +
<--+ ,
ApplicationUser--, ;
>--; <
>--< =
(--= >
)--> ?
;// 
services00 
.00 
AddAuthorization00 %
(00% &
options00& -
=>00. 0
{11 
options22 
.22 
	AddPolicy22 !
(22! "
$str22" /
,22/ 0
policy221 7
=>228 :
{33 
policy44 
.44 
RequireClaim44 '
(44' (
$str44( 2
)442 3
;443 4
policy55 
.55 
RequireClaim55 '
(55' (
$str55( 3
)553 4
;554 5
}66 
)66 
;66 
options77 
.77 
	AddPolicy77 !
(77! "
$str77" .
,77. /
policy770 6
=>777 9
policy77: @
.77@ A
RequireClaim77A M
(77M N
$str77N [
)77[ \
)77\ ]
;77] ^
}88 
)88 
;99 
}:: 	
	protected<< 
void<< 
ConfigJWTToken<< %
(<<% &
IServiceCollection<<& 8
services<<9 A
,<<A B
IConfiguration<<C Q
Configuration<<R _
)<<_ `
{== 	
services>> 
.>> 
AddAuthentication>> &
(>>& '
JwtBearerDefaults>>' 8
.>>8 9 
AuthenticationScheme>>9 M
)>>M N
.?? 
AddJwtBearer?? !
(??! "
options??" )
=>??* ,
{@@ 
optionsAA 
.AA  %
TokenValidationParametersAA  9
=AA: ;
newBB  %
TokenValidationParametersBB! :
{CC 
ValidateIssuerDD! /
=DD0 1
falseDD2 7
,DD7 8
ValidateAudienceEE! 1
=EE2 3
falseEE4 9
,EE9 :
ValidateLifetimeFF! 1
=FF2 3
trueFF4 8
,FF8 9$
ValidateIssuerSigningKeyGG! 9
=GG: ;
trueGG< @
,GG@ A
ValidIssuerHH! ,
=HH- .

MixServiceHH/ 9
.HH9 :
GetAuthConfigHH: G
<HHG H
stringHHH N
>HHN O
(HHO P
$strHHP X
)HHX Y
,HHY Z
ValidAudienceII! .
=II/ 0

MixServiceII1 ;
.II; <
GetAuthConfigII< I
<III J
stringIIJ P
>IIP Q
(IIQ R
$strIIR \
)II\ ]
,II] ^
IssuerSigningKeyJJ! 1
=JJ2 3
JwtSecurityKeyJJ4 B
.JJB C
CreateJJC I
(JJI J

MixServiceJJJ T
.JJT U
GetAuthConfigJJU b
<JJb c
stringJJc i
>JJi j
(JJj k
$strJJk v
)JJv w
)JJw x
}KK 
;KK 
optionsLL 
.LL  
EventsLL  &
=LL' (
newLL) ,
JwtBearerEventsLL- <
{MM "
OnAuthenticationFailedNN 2
=NN3 4
contextNN5 <
=>NN= ?
{OO 
ConsolePP  '
.PP' (
	WriteLinePP( 1
(PP1 2
$strPP2 L
+PPM N
contextPPO V
.PPV W
	ExceptionPPW `
.PP` a
MessagePPa h
)PPh i
;PPi j
returnQQ  &
TaskQQ' +
.QQ+ ,
CompletedTaskQQ, 9
;QQ9 :
}RR 
,RR 
OnTokenValidatedSS ,
=SS- .
contextSS/ 6
=>SS7 9
{TT 
ConsoleUU  '
.UU' (
	WriteLineUU( 1
(UU1 2
$strUU2 F
+UUG H
contextUUI P
.UUP Q
SecurityTokenUUQ ^
)UU^ _
;UU_ `
returnVV  &
TaskVV' +
.VV+ ,
CompletedTaskVV, 9
;VV9 :
}WW 
}XX 
;XX 
}YY 
)YY 
;YY 
}ZZ 	
	protected\\ 
void\\ 
ConfigCookieAuth\\ '
(\\' (
IServiceCollection\\( :
services\\; C
,\\C D
IConfiguration\\E S
Configuration\\T a
)\\a b
{]] 	
services^^ 
.^^ &
ConfigureApplicationCookie^^ /
(^^/ 0
options^^0 7
=>^^8 :
{__ 
optionsaa 
.aa 
Cookieaa 
.aa 
HttpOnlyaa '
=aa( )
trueaa* .
;aa. /
optionsbb 
.bb 
Cookiebb 
.bb 

Expirationbb )
=bb* +
TimeSpanbb, 4
.bb4 5
FromDaysbb5 =
(bb= >
$numbb> A
)bbA B
;bbB C
optionscc 
.cc 
	LoginPathcc !
=cc" #
$strcc$ '
+cc( )

MixServicecc* 4
.cc4 5
	GetConfigcc5 >
<cc> ?
stringcc? E
>ccE F
(ccF G
MixConstantsccG S
.ccS T 
ConfigurationKeywordccT h
.cch i
DefaultCulturecci w
)ccw x
+ccy z
$str	cc{ �
;
cc� �
optionsdd 
.dd 

LogoutPathdd "
=dd# $
$strdd% (
+dd) *

MixServicedd+ 5
.dd5 6
	GetConfigdd6 ?
<dd? @
stringdd@ F
>ddF G
(ddG H
MixConstantsddH T
.ddT U 
ConfigurationKeywordddU i
.ddi j
DefaultCultureddj x
)ddx y
+ddz {
$str	dd| �
;
dd� �
optionsee 
.ee 
AccessDeniedPathee (
=ee) *
$stree+ .
;ee. /
optionsff 
.ff 
SlidingExpirationff )
=ff* +
trueff, 0
;ff0 1
}gg 
)gg 
;gg 
servicesii 
.ii 
AddAuthenticationii &
(ii& '(
CookieAuthenticationDefaultsii' C
.iiC D 
AuthenticationSchemeiiD X
)iiX Y
.jj 
	AddCookiejj 
(jj 
optionskk 
=>kk 
{ll 
optionsnn 
.nn 
Cookienn "
.nn" #
HttpOnlynn# +
=nn, -
truenn. 2
;nn2 3
optionsoo 
.oo 
Cookieoo "
.oo" #

Expirationoo# -
=oo. /
TimeSpanoo0 8
.oo8 9
FromDaysoo9 A
(ooA B
$numooB E
)ooE F
;ooF G
optionspp 
.pp 
	LoginPathpp %
=pp& '
$strpp( +
+pp, -

MixServicepp. 8
.pp8 9
	GetConfigpp9 B
<ppB C
stringppC I
>ppI J
(ppJ K
MixConstantsppK W
.ppW X 
ConfigurationKeywordppX l
.ppl m
DefaultCultureppm {
)pp{ |
+pp} ~
$str	pp �
;
pp� �
optionsqq 
.qq 

LogoutPathqq &
=qq' (
$strqq) ,
+qq- .

MixServiceqq/ 9
.qq9 :
	GetConfigqq: C
<qqC D
stringqqD J
>qqJ K
(qqK L
MixConstantsqqL X
.qqX Y 
ConfigurationKeywordqqY m
.qqm n
DefaultCultureqqn |
)qq| }
+qq~ 
$str
qq� �
;
qq� �
optionsrr 
.rr 
AccessDeniedPathrr ,
=rr- .
$strrr/ 2
;rr2 3
optionsss 
.ss 
SlidingExpirationss -
=ss. /
truess0 4
;ss4 5
optionsuu 
.uu 
Eventsuu "
=uu# $
newuu% (&
CookieAuthenticationEventsuu) C
(uuC D
)uuD E
{vv 
OnValidatePrincipalww +
=ww, -
CookieValidatorww. =
.ww= >
ValidateAsyncww> K
}xx 
;xx 
}yy 
)zz 
;zz 
}{{ 	
	protected}} 
static}} 
class}} 
JwtSecurityKey}} -
{~~ 	
public 
static  
SymmetricSecurityKey .
Create/ 5
(5 6
string6 <
secret= C
)C D
{
�� 
return
�� 
new
�� "
SymmetricSecurityKey
�� /
(
��/ 0
Encoding
��0 8
.
��8 9
ASCII
��9 >
.
��> ?
GetBytes
��? G
(
��G H
secret
��H N
)
��N O
)
��O P
;
��P Q
}
�� 
}
�� 	
}
�� 
}�� �
RC:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\App_Start\Startup.SignalR.cs
	namespace		 	
Mix		
 
.		 
Cms		 
.		 
Web		 
{

 
public 

partial 
class 
Startup  
{ 
public 
void $
ConfigureSignalRServices ,
(, -
IServiceCollection- ?
services@ H
)H I
{ 	
} 	
public 
void  
ConfigurationSignalR (
(( )
IApplicationBuilder) <
app= @
)@ A
{ 	
} 	
} 
} �
^C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\App_Start\Validattors\CookieValidator.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
. 
Mvc 
. 
	App_Start #
.# $
Validattors$ /
{ 
public		 

class		 
CookieValidator		  
{

 
public 
static 
async 
Task  
ValidateAsync! .
(. /*
CookieValidatePrincipalContext/ M
contextN U
)U V
{ 	
await 
Task 
. 
CompletedTask $
;$ %
} 	
} 
} �	
[C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\App_Start\Validattors\JwtValidator.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
. 
Mvc 
. 
	App_Start #
.# $
Validattors$ /
{		 
public

 

class

 
JwtValidator

 
{ 
public 
static 
void 
ValidateAsync (
(( )!
TokenValidatedContext) >
context? F
)F G
{ 	
Console 
. 
	WriteLine 
( 
$str 2
+3 4
context5 <
.< =
SecurityToken= J
)J K
;K L
} 	
public 
static 
void 
ValidateFail '
(' ('
AuthenticationFailedContext( C
contextD K
)K L
{ 	
Console 
. 
	WriteLine 
( 
$str 8
+9 :
context; B
.B C
	ExceptionC L
.L M
MessageM T
)T U
;U V
} 	
} 
} �
SC:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\Controllers\BaseController.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
. 
Controllers !
{ 
public 

class 
BaseController 
:  !

Controller" ,
{ 
	protected 
string 
_domain  
;  !
	protected 
IConfiguration  
_configuration! /
;/ 0
	protected 
IHostingEnvironment %
_env& *
;* +
	protected 
readonly 
IMemoryCache '
_memoryCache( 4
;4 5
public 
BaseController 
( 
IHostingEnvironment 1
env2 5
,5 6
IMemoryCache7 C
memoryCacheD O
)O P
{ 	
_env 
= 
env 
; 
_memoryCache 
= 
memoryCache &
;& '
var 
cultureInfo 
= 
new !
CultureInfo" -
(- .
_culture. 6
)6 7
;7 8
CultureInfo 
. '
DefaultThreadCurrentCulture 3
=4 5
cultureInfo6 A
;A B
CultureInfo 
. )
DefaultThreadCurrentUICulture 5
=6 7
cultureInfo8 C
;C D
} 	
public 
BaseController 
( 
IHostingEnvironment 1
env2 5
,5 6
IConfiguration7 E
configurationF S
)S T
{ 	
_configuration   
=   
configuration   *
;  * +
_env!! 
=!! 
env!! 
;!! 
var## 
cultureInfo## 
=## 
new## !
CultureInfo##" -
(##- .
_culture##. 6
)##6 7
;##7 8
CultureInfo$$ 
.$$ '
DefaultThreadCurrentCulture$$ 3
=$$4 5
cultureInfo$$6 A
;$$A B
CultureInfo%% 
.%% )
DefaultThreadCurrentUICulture%% 5
=%%6 7
cultureInfo%%8 C
;%%C D
}&& 	
public(( 
ViewContext(( 
ViewContext(( &
{((' (
get(() ,
;((, -
set((. 1
;((1 2
}((3 4
	protected** 
string** 
_culture** !
{++ 	
get,, 
=>,, 
	RouteData,, 
?,, 
.,, 
Values,, $
[,,$ %
$str,,% .
],,. /
?,,/ 0
.,,0 1
ToString,,1 9
(,,9 :
),,: ;
.,,; <
ToLower,,< C
(,,C D
),,D E
??,,F H

MixService,,I S
.,,S T
	GetConfig,,T ]
<,,] ^
string,,^ d
>,,d e
(,,e f
$str,,f v
),,v w
;,,w x
}-- 	
public// 
override// 
void// 
OnActionExecuting// .
(//. /"
ActionExecutingContext/// E
context//F M
)//M N
{00 	
ViewBag11 
.11 
culture11 
=11 
_culture11 &
;11& '
_domain22 
=22 
string22 
.22 
Format22 #
(22# $
$str22$ /
,22/ 0
Request221 8
.228 9
Scheme229 ?
,22? @
Request22A H
.22H I
Host22I M
)22M N
;22N O
base33 
.33 
OnActionExecuting33 "
(33" #
context33# *
)33* +
;33+ ,
}44 	
	protected66 
void66 
GetLanguage66 "
(66" #
)66# $
{77 	
}99 	
};; 
}<< �
SC:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\Controllers\HomeController.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
. 
Controllers !
{ 
public 

class 
HomeController 
:  !
BaseController" 0
{ 
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
public 
HomeController 
( 
IHostingEnvironment 1
env2 5
,5 6
IMemoryCache 
memoryCache $
,$ %
UserManager 
< 
ApplicationUser (
>( )
userManager* 5
) 
: 
base 
( 
env 
, 
memoryCache %
)% &
{ 	
this 
. 
_userManager 
= 
userManager  +
;+ ,
} 	
[ 	
Route	 
( 
$str 
) 
] 
[ 	
Route	 
( 
$str 
) 
] 
[   	
Route  	 
(   
$str   $
)  $ %
]  % &
public!! 
async!! 
System!! 
.!! 
	Threading!! %
.!!% &
Tasks!!& +
.!!+ ,
Task!!, 0
<!!0 1
IActionResult!!1 >
>!!> ?
Index!!@ E
(!!E F
string!!F L
culture!!M T
,!!T U
string!!V \
seoName!!] d
)!!d e
{"" 	
if## 
(## 

MixService## 
.## 
	GetConfig## $
<##$ %
bool##% )
>##) *
(##* +
$str##+ 3
)##3 4
)##4 5
{$$ 
return&& 
await&& 
	PageAsync&& &
(&&& '
seoName&&' .
)&&. /
;&&/ 0
})) 
else** 
{++ 
if,, 
(,, 
string,, 
.,, 
IsNullOrEmpty,, (
(,,( )

MixService,,) 3
.,,3 4
GetConnectionString,,4 G
(,,G H
MixConstants,,H T
.,,T U 
CONST_CMS_CONNECTION,,U i
),,i j
),,j k
),,k l
{-- 
return.. 
Redirect.. #
(..# $
$str..$ *
)..* +
;..+ ,
}// 
else00 
{11 
return22 
Redirect22 #
(22# $
$"22$ &
/init/step222& 1
"221 2
)222 3
;223 4
}33 
}44 
}55 	
[77 	
HttpGet77	 
]77 
[88 	
Route88	 
(88 
$str88 
)88 
]88 
[99 	
Route99	 
(99 
$str99 
)99 
]99 
[:: 	
Route::	 
(:: 
$str:: "
)::" #
]::# $
[;; 	
Route;;	 
(;; 
$str;; )
);;) *
];;* +
[<< 	
Route<<	 
(<< 
$str<< 1
)<<1 2
]<<2 3
[== 	
Route==	 
(== 
$str== :
)==: ;
]==; <
[>> 	
Route>>	 
(>> 
$str>> C
)>>C D
]>>D E
[?? 	
Route??	 
(?? 
$str?? L
)??L M
]??M N
[@@ 	
Route@@	 
(@@ 
$str@@ U
)@@U V
]@@V W
publicAA 
IActionResultAA 
PortalAA #
(AA# $
)AA$ %
{BB 	
returnCC 
ViewCC 
(CC 
)CC 
;CC 
}DD 	
[FF 	
HttpGetFF	 
]FF 
[GG 	
RouteGG	 
(GG 
$strGG 
)GG 
]GG 
[HH 	
RouteHH	 
(HH 
$strHH 
)HH 
]HH 
publicII 
IActionResultII 
InitII !
(II! "
stringII" (
pageII) -
)II- .
{JJ 	
ifKK 
(KK 
stringKK 
.KK 
IsNullOrEmptyKK $
(KK$ %
pageKK% )
)KK) *
&&KK+ -

MixServiceKK. 8
.KK8 9
	GetConfigKK9 B
<KKB C
boolKKC G
>KKG H
(KKH I
$strKKI Q
)KKQ R
)KKR S
{LL 
returnMM 
RedirectMM 
(MM  
$"MM  "
/init/loginMM" -
"MM- .
)MM. /
;MM/ 0
}NN 
elseOO 
{PP 
returnQQ 
ViewQQ 
(QQ 
)QQ 
;QQ 
}RR 
}TT 	
[VV 	
HttpGetVV	 
]VV 
[WW 	
RouteWW	 
(WW 
$strWW 
)WW 
]WW 
publicXX 
asyncXX 
SystemXX 
.XX 
	ThreadingXX %
.XX% &
TasksXX& +
.XX+ ,
TaskXX, 0
<XX0 1
IActionResultXX1 >
>XX> ?
PageNotFoundXX@ L
(XXL M
)XXM N
{YY 	
returnZZ 
awaitZZ 
	PageAsyncZZ "
(ZZ" #
$strZZ# (
)ZZ( )
;ZZ) *
}[[ 	
async^^ 
System^^ 
.^^ 
	Threading^^ 
.^^ 
Tasks^^ $
.^^$ %
Task^^% )
<^^) *
IActionResult^^* 7
>^^7 8
	PageAsync^^9 B
(^^B C
string^^C I
seoName^^J Q
)^^Q R
{__ 	
varbb 
getPagebb 
=bb 
newbb 
RepositoryResponsebb 0
<bb0 1
Libbb1 4
.bb4 5

ViewModelsbb5 ?
.bb? @
MixPagesbb@ H
.bbH I
ReadMvcViewModelbbI Y
>bbY Z
(bbZ [
)bb[ \
;bb\ ]
vardd 
cacheKeydd 
=dd 
$"dd 
Page_dd "
{dd" #
_culturedd# +
}dd+ ,
_dd, -
{dd- .
seoNamedd. 5
}dd5 6
"dd6 7
;dd7 8
varee 
dataee 
=ee 
_memoryCacheee #
.ee# $
Getee$ '
<ee' (
Libee( +
.ee+ ,

ViewModelsee, 6
.ee6 7
MixPagesee7 ?
.ee? @
ReadMvcViewModelee@ P
>eeP Q
(eeQ R
cacheKeyeeR Z
)eeZ [
;ee[ \
ifff 
(ff 
dataff 
!=ff 
nullff 
)ff 
{gg 
getPagehh 
.hh 
	IsSucceedhh !
=hh" #
truehh$ (
;hh( )
getPageii 
.ii 
Dataii 
=ii 
dataii #
;ii# $
}jj 
elsekk 
{ll 

Expressionmm 
<mm 
Funcmm 
<mm  
MixPagemm  '
,mm' (
boolmm) -
>mm- .
>mm. /
	predicatemm0 9
;mm9 :
ifnn 
(nn 
stringnn 
.nn 
IsNullOrEmptynn (
(nn( )
seoNamenn) 0
)nn0 1
)nn1 2
{oo 
	predicatepp 
=pp 
ppp  
=>pp! #
pqq 
.qq 
Typeqq 
==qq 
(qq 
intqq !
)qq! "
MixPageTypeqq" -
.qq- .
Homeqq. 2
&&rr 
prr 
.rr 
Statusrr 
==rr !
(rr" #
intrr# &
)rr& '
MixContentStatusrr' 7
.rr7 8
	Publishedrr8 A
&&rrB D
prrE F
.rrF G
SpecificulturerrG U
==rrV X
_culturerrY a
;rra b
}ss 
elsett 
{uu 
	predicatevv 
=vv 
pvv  !
=>vv" $
pww 
.ww 
SeoNameww 
==ww  
seoNameww! (
&&xx 
pxx 
.xx 
Statusxx 
==xx  "
(xx# $
intxx$ '
)xx' (
MixContentStatusxx( 8
.xx8 9
	Publishedxx9 B
&&xxC E
pxxF G
.xxG H
SpecificulturexxH V
==xxW Y
_culturexxZ b
;xxb c
}yy 
getPage{{ 
={{ 
await{{ 
Lib{{  #
.{{# $

ViewModels{{$ .
.{{. /
MixPages{{/ 7
.{{7 8
ReadMvcViewModel{{8 H
.{{H I

Repository{{I S
.{{S T
GetSingleModelAsync{{T g
({{g h
	predicate{{h q
){{q r
;{{r s
_memoryCache|| 
.|| 
Set||  
(||  !
cacheKey||! )
,||) *
getPage||+ 2
.||2 3
Data||3 7
)||7 8
;||8 9
}}} 
if 
( 
getPage 
. 
	IsSucceed !
&&" $
getPage% ,
., -
Data- 1
.1 2
View2 6
!=7 9
null: >
)> ?
{
�� %
GeneratePageDetailsUrls
�� '
(
��' (
getPage
��( /
.
��/ 0
Data
��0 4
)
��4 5
;
��5 6
ViewData
�� 
[
�� 
$str
��  
]
��  !
=
��" #
getPage
��$ +
.
��+ ,
Data
��, 0
.
��0 1
SeoTitle
��1 9
;
��9 :
ViewData
�� 
[
�� 
$str
�� &
]
��& '
=
��( )
getPage
��* 1
.
��1 2
Data
��2 6
.
��6 7
SeoDescription
��7 E
;
��E F
ViewData
�� 
[
�� 
$str
�� #
]
��# $
=
��% &
getPage
��' .
.
��. /
Data
��/ 3
.
��3 4
SeoKeywords
��4 ?
;
��? @
ViewData
�� 
[
�� 
$str
��  
]
��  !
=
��" #
getPage
��$ +
.
��+ ,
Data
��, 0
.
��0 1
ImageUrl
��1 9
;
��9 :
ViewData
�� 
[
�� 
$str
�� $
]
��$ %
=
��& '
getPage
��( /
.
��/ 0
Data
��0 4
.
��4 5
CssClass
��5 =
;
��= >
return
�� 
View
�� 
(
�� 
getPage
�� #
.
��# $
Data
��$ (
)
��( )
;
��) *
}
�� 
else
�� 
{
�� 
return
�� 
NotFound
�� 
(
��  
)
��  !
;
��! "
}
�� 
}
�� 	
IActionResult
�� 
ArticleView
�� !
(
��! "

Expression
��" ,
<
��, -
Func
��- 1
<
��1 2

MixArticle
��2 <
,
��< =
bool
��> B
>
��B C
>
��C D
	predicate
��E N
)
��N O
{
�� 	
var
�� 

getArticle
�� 
=
�� 
Lib
��  
.
��  !

ViewModels
��! +
.
��+ ,
MixArticles
��, 7
.
��7 8
ReadMvcViewModel
��8 H
.
��H I

Repository
��I S
.
��S T
GetSingleModel
��T b
(
��b c
	predicate
��c l
)
��l m
;
��m n
if
�� 
(
�� 

getArticle
�� 
.
�� 
	IsSucceed
�� $
)
��$ %
{
�� 
ViewData
�� 
[
�� 
$str
��  
]
��  !
=
��" #

getArticle
��$ .
.
��. /
Data
��/ 3
.
��3 4
SeoTitle
��4 <
;
��< =
ViewData
�� 
[
�� 
$str
�� &
]
��& '
=
��( )

getArticle
��* 4
.
��4 5
Data
��5 9
.
��9 :
SeoDescription
��: H
;
��H I
ViewData
�� 
[
�� 
$str
�� #
]
��# $
=
��% &

getArticle
��' 1
.
��1 2
Data
��2 6
.
��6 7
SeoKeywords
��7 B
;
��B C
ViewData
�� 
[
�� 
$str
��  
]
��  !
=
��" #

getArticle
��$ .
.
��. /
Data
��/ 3
.
��3 4
ImageUrl
��4 <
;
��< =
return
�� 
View
�� 
(
�� 

getArticle
�� &
.
��& '
Data
��' +
)
��+ ,
;
��, -
}
�� 
else
�� 
{
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( 6
,
��6 7
$str
��8 >
)
��> ?
;
��? @
}
�� 
}
�� 	
IActionResult
�� 
ProductView
�� !
(
��! "

Expression
��" ,
<
��, -
Func
��- 1
<
��1 2

MixProduct
��2 <
,
��< =
bool
��> B
>
��B C
>
��C D
	predicate
��E N
)
��N O
{
�� 	
var
�� 

getProduct
�� 
=
�� 
Lib
��  
.
��  !

ViewModels
��! +
.
��+ ,
MixProducts
��, 7
.
��7 8
ReadMvcViewModel
��8 H
.
��H I

Repository
��I S
.
��S T
GetSingleModel
��T b
(
��b c
	predicate
��c l
)
��l m
;
��m n
if
�� 
(
�� 

getProduct
�� 
.
�� 
	IsSucceed
�� $
)
��$ %
{
�� 

getProduct
�� 
.
�� 
Data
�� 
.
��  
ProductNavs
��  +
.
��+ ,
ForEach
��, 3
(
��3 4
p
��4 5
=>
��6 8
{
�� 
p
�� 
.
�� 
RelatedProduct
�� $
.
��$ %

DetailsUrl
��% /
=
��0 1 
GenerateDetailsUrl
��2 D
(
��D E
$str
��E N
,
��N O
new
��P S
{
��T U
seoName
��V ]
=
��^ _
p
��` a
.
��a b
RelatedProduct
��b p
.
��p q
SeoName
��q x
}
��y z
)
��z {
;
��{ |
}
�� 
)
�� 
;
�� 
ViewData
�� 
[
�� 
$str
��  
]
��  !
=
��" #

getProduct
��$ .
.
��. /
Data
��/ 3
.
��3 4
SeoTitle
��4 <
;
��< =
ViewData
�� 
[
�� 
$str
�� &
]
��& '
=
��( )

getProduct
��* 4
.
��4 5
Data
��5 9
.
��9 :
SeoDescription
��: H
;
��H I
ViewData
�� 
[
�� 
$str
�� #
]
��# $
=
��% &

getProduct
��' 1
.
��1 2
Data
��2 6
.
��6 7
SeoKeywords
��7 B
;
��B C
ViewData
�� 
[
�� 
$str
��  
]
��  !
=
��" #

getProduct
��$ .
.
��. /
Data
��/ 3
.
��3 4
ImageUrl
��4 <
;
��< =
return
�� 
View
�� 
(
�� 

getProduct
�� &
.
��& '
Data
��' +
)
��+ ,
;
��, -
}
�� 
else
�� 
{
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( 6
,
��6 7
$str
��8 >
)
��> ?
;
��? @
}
�� 
}
�� 	
void
�� %
GeneratePageDetailsUrls
�� $
(
��$ %
Lib
��% (
.
��( )

ViewModels
��) 3
.
��3 4
MixPages
��4 <
.
��< =
ReadMvcViewModel
��= M
page
��N R
)
��R S
{
�� 	
foreach
�� 
(
�� 
var
�� 

articleNav
�� #
in
��$ &
page
��' +
.
��+ ,
Articles
��, 4
.
��4 5
Items
��5 :
)
��: ;
{
�� 
if
�� 
(
�� 

articleNav
�� 
.
�� 
Article
�� &
!=
��' )
null
��* .
)
��. /
{
�� 

articleNav
�� 
.
�� 
Article
�� &
.
��& '

DetailsUrl
��' 1
=
��2 3 
GenerateDetailsUrl
��4 F
(
��F G
$str
��G P
,
��P Q
new
��R U
{
��V W
seoName
��X _
=
��` a

articleNav
��b l
.
��l m
Article
��m t
.
��t u
SeoName
��u |
}
��} ~
)
��~ 
;�� �
}
�� 
}
�� 
foreach
�� 
(
�� 
var
�� 

productNav
�� #
in
��$ &
page
��' +
.
��+ ,
Products
��, 4
.
��4 5
Items
��5 :
)
��: ;
{
�� 
if
�� 
(
�� 

productNav
�� 
.
�� 
Product
�� &
!=
��' )
null
��* .
)
��. /
{
�� 

productNav
�� 
.
�� 
Product
�� &
.
��& '

DetailsUrl
��' 1
=
��2 3 
GenerateDetailsUrl
��4 F
(
��F G
$str
��G P
,
��P Q
new
��R U
{
��V W
seoName
��X _
=
��` a

productNav
��b l
.
��l m
Product
��m t
.
��t u
SeoName
��u |
}
��} ~
)
��~ 
;�� �
}
�� 
}
�� 
}
�� 	
void
�� %
GeneratePageDetailsUrls
�� $
(
��$ %
Lib
��% (
.
��( )

ViewModels
��) 3
.
��3 4

MixModules
��4 >
.
��> ?
ReadMvcViewModel
��? O
module
��P V
)
��V W
{
�� 	
foreach
�� 
(
�� 
var
�� 

articleNav
�� #
in
��$ &
module
��' -
.
��- .
Articles
��. 6
.
��6 7
Items
��7 <
)
��< =
{
�� 
if
�� 
(
�� 

articleNav
�� 
.
�� 
Article
�� &
!=
��' )
null
��* .
)
��. /
{
�� 

articleNav
�� 
.
�� 
Article
�� &
.
��& '

DetailsUrl
��' 1
=
��2 3 
GenerateDetailsUrl
��4 F
(
��F G
$str
��G P
,
��P Q
new
��R U
{
��V W
seoName
��X _
=
��` a

articleNav
��b l
.
��l m
Article
��m t
.
��t u
SeoName
��u |
}
��} ~
)
��~ 
;�� �
}
�� 
}
�� 
foreach
�� 
(
�� 
var
�� 

productNav
�� #
in
��$ &
module
��' -
.
��- .
Products
��. 6
.
��6 7
Items
��7 <
)
��< =
{
�� 
if
�� 
(
�� 

productNav
�� 
.
�� 
Product
�� &
!=
��' )
null
��* .
)
��. /
{
�� 

productNav
�� 
.
�� 
Product
�� &
.
��& '

DetailsUrl
��' 1
=
��2 3 
GenerateDetailsUrl
��4 F
(
��F G
$str
��G P
,
��P Q
new
��R U
{
��V W
seoName
��X _
=
��` a

productNav
��b l
.
��l m
Product
��m t
.
��t u
SeoName
��u |
}
��} ~
)
��~ 
;�� �
}
�� 
}
�� 
}
�� 	
string
��  
GenerateDetailsUrl
�� !
(
��! "
string
��" (
type
��) -
,
��- .
object
��/ 5
routeValues
��6 A
)
��A B
{
�� 	
return
�� 
MixCmsHelper
�� 
.
��  
GetRouterUrl
��  ,
(
��, -
type
��- 1
,
��1 2
routeValues
��3 >
,
��> ?
Request
��@ G
,
��G H
Url
��I L
)
��L M
;
��M N
}
�� 	
[
�� 	
ResponseCache
��	 
(
�� 
Duration
�� 
=
��  !
$num
��" #
,
��# $
Location
��% -
=
��. /#
ResponseCacheLocation
��0 E
.
��E F
None
��F J
,
��J K
NoStore
��L S
=
��T U
true
��V Z
)
��Z [
]
��[ \
public
�� 
IActionResult
�� 
Error
�� "
(
��" #
)
��# $
{
�� 	
return
�� 
View
�� 
(
�� 
new
�� 
ErrorViewModel
�� *
{
��+ ,
	RequestId
��- 6
=
��7 8
Activity
��9 A
.
��A B
Current
��B I
?
��I J
.
��J K
Id
��K M
??
��N P
HttpContext
��Q \
.
��\ ]
TraceIdentifier
��] l
}
��m n
)
��n o
;
��o p
}
�� 	
}
�� 
}�� �
NC:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\Models\ErrorViewModel.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
. 
Models 
{ 
public 

class 
ErrorViewModel 
{ 
public 
string 
	RequestId 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
bool		 
ShowRequestId		 !
=>		" $
!		% &
string		& ,
.		, -
IsNullOrEmpty		- :
(		: ;
	RequestId		; D
)		D E
;		E F
}

 
} �
@C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\Program.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	 
CreateWebHostBuilder  
(  !
args! %
)% &
.& '
Build' ,
(, -
)- .
.. /
Run/ 2
(2 3
)3 4
;4 5
} 	
public 
static 
IWebHostBuilder % 
CreateWebHostBuilder& :
(: ;
string; A
[A B
]B C
argsD H
)H I
{ 	
var 
config 
= 
new  
ConfigurationBuilder 1
(1 2
)2 3
. 
SetBasePath 
( 
	Directory !
.! "
GetCurrentDirectory" 5
(5 6
)6 7
)7 8
. 
AddJsonFile 
( 
$str -
,- .
optional/ 7
:7 8
true9 =
,= >
reloadOnChange? M
:M N
trueO S
)S T
. 
Build 
( 
) 
; 
return 
WebHost 
.  
CreateDefaultBuilder /
(/ 0
args0 4
)4 5
. 
UseConfiguration !
(! "
config" (
)( )
. 

UseStartup 
< 
Startup #
># $
($ %
)% &
;& '
} 	
} 
}   �.
EC:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\RewriteRules.cs
	namespace 	
RewriteRules
 
{ 
public		 

class		 
MethodRules		 
{

 
public 
static 
void 
RedirectXMLRequests .
(. /
RewriteContext/ =
context> E
)E F
{ 	
var 
request 
= 
context !
.! "
HttpContext" -
.- .
Request. 5
;5 6
if 
( 
request 
. 
Path 
. 
StartsWithSegments /
(/ 0
new0 3

PathString4 >
(> ?
$str? J
)J K
)K L
)L M
{ 
return 
; 
} 
if 
( 
request 
. 
Path 
. 
Value "
." #
EndsWith# +
(+ ,
$str, 2
,2 3
StringComparison4 D
.D E
OrdinalIgnoreCaseE V
)V W
)W X
{ 
var 
response 
= 
context &
.& '
HttpContext' 2
.2 3
Response3 ;
;; <
response 
. 

StatusCode #
=$ %
StatusCodes& 1
.1 2%
Status301MovedPermanently2 K
;K L
context 
. 
Result 
=  

RuleResult! +
.+ ,
EndResponse, 7
;7 8
response 
. 
Headers  
[  !
HeaderNames! ,
., -
Location- 5
]5 6
=7 8
$str 
+  !
request" )
.) *
Path* .
+/ 0
request1 8
.8 9
QueryString9 D
;D E
} 
} 	
}!! 
public$$ 

class$$ !
RedirectImageRequests$$ &
:$$' (
IRule$$) .
{%% 
private&& 
readonly&& 
string&& 

_extension&&  *
;&&* +
private'' 
readonly'' 

PathString'' #
_newPath''$ ,
;'', -
public)) !
RedirectImageRequests)) $
())$ %
string))% +
	extension)), 5
,))5 6
string))7 =
newPath))> E
)))E F
{** 	
if++ 
(++ 
string++ 
.++ 
IsNullOrEmpty++ $
(++$ %
	extension++% .
)++. /
)++/ 0
{,, 
throw-- 
new-- 
ArgumentException-- +
(--+ ,
nameof--, 2
(--2 3
	extension--3 <
)--< =
)--= >
;--> ?
}.. 
if00 
(00 
!00 
Regex00 
.00 
IsMatch00 
(00 
	extension00 (
,00( )
$str00* >
)00> ?
)00? @
{11 
throw22 
new22 
ArgumentException22 +
(22+ ,
$str22, ?
,22? @
nameof22A G
(22G H
	extension22H Q
)22Q R
)22R S
;22S T
}33 
if55 
(55 
!55 
Regex55 
.55 
IsMatch55 
(55 
newPath55 &
,55& '
$str55( <
)55< =
)55= >
{66 
throw77 
new77 
ArgumentException77 +
(77+ ,
$str77, :
,77: ;
nameof77< B
(77B C
newPath77C J
)77J K
)77K L
;77L M
}88 

_extension:: 
=:: 
	extension:: "
;::" #
_newPath;; 
=;; 
new;; 

PathString;; %
(;;% &
newPath;;& -
);;- .
;;;. /
}<< 	
public>> 
void>> 
	ApplyRule>> 
(>> 
RewriteContext>> ,
context>>- 4
)>>4 5
{?? 	
var@@ 
request@@ 
=@@ 
context@@ !
.@@! "
HttpContext@@" -
.@@- .
Request@@. 5
;@@5 6
ifDD 
(DD 
requestDD 
.DD 
PathDD 
.DD 
StartsWithSegmentsDD /
(DD/ 0
newDD0 3

PathStringDD4 >
(DD> ?
_newPathDD? G
)DDG H
)DDH I
)DDI J
{EE 
returnFF 
;FF 
}GG 
ifII 
(II 
requestII 
.II 
PathII 
.II 
ValueII "
.II" #
EndsWithII# +
(II+ ,

_extensionII, 6
,II6 7
StringComparisonII8 H
.IIH I
OrdinalIgnoreCaseIII Z
)IIZ [
)II[ \
{JJ 
varKK 
responseKK 
=KK 
contextKK &
.KK& '
HttpContextKK' 2
.KK2 3
ResponseKK3 ;
;KK; <
responseLL 
.LL 

StatusCodeLL #
=LL$ %
StatusCodesLL& 1
.LL1 2%
Status301MovedPermanentlyLL2 K
;LLK L
contextMM 
.MM 
ResultMM 
=MM  

RuleResultMM! +
.MM+ ,
EndResponseMM, 7
;MM7 8
responseNN 
.NN 
HeadersNN  
[NN  !
HeaderNamesNN! ,
.NN, -
LocationNN- 5
]NN5 6
=NN7 8
_newPathOO 
+OO 
requestOO &
.OO& '
PathOO' +
+OO, -
requestOO. 5
.OO5 6
QueryStringOO6 A
;OOA B
}PP 
}QQ 	
}RR 
}TT �_
@C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Web\Startup.cs
	namespace 	
Mix
 
. 
Cms 
. 
Web 
{ 
public 

partial 
class 
Startup  
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public   
void   
ConfigureServices   %
(  % &
IServiceCollection  & 8
services  9 A
)  A B
{!! 	
services"" 
."" 
	Configure"" 
<"" 
CookiePolicyOptions"" 2
>""2 3
(""3 4
options""4 ;
=>""< >
{## 
options%% 
.%% 
CheckConsentNeeded%% *
=%%+ ,
context%%- 4
=>%%5 7
true%%8 <
;%%< =
options&& 
.&& !
MinimumSameSitePolicy&& -
=&&. /
SameSiteMode&&0 <
.&&< =
None&&= A
;&&A B
}'' 
)'' 
;'' 
services** 
.** 
	Configure** 
<** 
CookiePolicyOptions** 2
>**2 3
(**3 4
options**4 ;
=>**< >
{++ 
options-- 
.-- 
CheckConsentNeeded-- *
=--+ ,
context--- 4
=>--5 7
true--8 <
;--< =
options.. 
... !
MinimumSameSitePolicy.. -
=... /
SameSiteMode..0 <
...< =
None..= A
;..A B
}// 
)// 
;// 
ConfigIdentity11 
(11 
services11 #
,11# $
Configuration11% 2
,112 3
MixConstants114 @
.11@ A 
CONST_CMS_CONNECTION11A U
)11U V
;11V W
ConfigCookieAuth22 
(22 
services22 %
,22% &
Configuration22' 4
)224 5
;225 6
ConfigJWTToken33 
(33 
services33 #
,33# $
Configuration33% 2
)332 3
;333 4
services55 
.55 
AddDbContext55 !
<55! "
MixCmsContext55" /
>55/ 0
(550 1
)551 2
;552 3
services77 
.77 
	Configure77 
<77 
WebEncoderOptions77 0
>770 1
(771 2
options772 9
=>77: <
options77= D
.77D E
TextEncoderSettings77E X
=77Y Z
new77[ ^
TextEncoderSettings77_ r
(77r s
UnicodeRanges	77s �
.
77� �
All
77� �
)
77� �
)
77� �
;
77� �
services88 
.88 
	Configure88 
<88 
FormOptions88 *
>88* +
(88+ ,
options88, 3
=>884 6
options887 >
.88> ?$
MultipartBodyLengthLimit88? W
=88X Y
$num88Z c
)88c d
;88d e
services;; 
.;; 
AddTransient;; !
<;;! "
IEmailSender;;" .
,;;. /"
AuthEmailMessageSender;;0 F
>;;F G
(;;G H
);;H I
;;;I J
services<< 
.<< 
AddTransient<< !
<<<! "

ISmsSender<<" ,
,<<, - 
AuthSmsMessageSender<<. B
><<B C
(<<C D
)<<D E
;<<E F
services== 
.== 
AddSingleton== !
<==! "

MixService==" ,
>==, -
(==- .
)==. /
;==/ 0
services@@ 
.@@ 

AddSignalR@@ 
(@@  
)@@  !
;@@! "
servicesBB 
.BB 
AddSwaggerGenBB "
(BB" #
cBB# $
=>BB% '
{CC 
cDD 
.DD 

SwaggerDocDD 
(DD 
$strDD !
,DD! "
newDD# &
InfoDD' +
{DD, -
TitleDD. 3
=DD4 5
$strDD6 H
,DDH I
VersionDDJ Q
=DDR S
$strDDT X
}DDY Z
)DDZ [
;DD[ \
}EE 
)EE 
;EE 
servicesFF 
.FF 
AddAuthenticationFF &
(FF& '
$strFF' /
)FF/ 0
;FF0 1
servicesHH 
.HH 
AddMvcHH 
(HH 
optionsHH #
=>HH$ &
{II 
optionsJJ 
.JJ 
CacheProfilesJJ %
.JJ% &
AddJJ& )
(JJ) *
$strJJ* 3
,JJ3 4
newKK 
CacheProfileKK $
(KK$ %
)KK% &
{LL 
DurationMM  
=MM! "
$numMM# %
}NN 
)NN 
;NN 
optionsOO 
.OO 
CacheProfilesOO %
.OO% &
AddOO& )
(OO) *
$strOO* 1
,OO1 2
newPP 
CacheProfilePP $
(PP$ %
)PP% &
{QQ 
LocationRR  
=RR! "!
ResponseCacheLocationRR# 8
.RR8 9
NoneRR9 =
,RR= >
NoStoreSS 
=SS  !
trueSS" &
}TT 
)TT 
;TT 
}UU 
)UU 
.UU #
SetCompatibilityVersionUU &
(UU& ' 
CompatibilityVersionUU' ;
.UU; <
Version_2_1UU< G
)UUG H
;UUH I
servicesWW 
.WW 
AddMemoryCacheWW #
(WW# $
)WW$ %
;WW% &
}YY 	
public\\ 
void\\ 
	Configure\\ 
(\\ 
IApplicationBuilder\\ 1
app\\2 5
,\\5 6
IHostingEnvironment\\7 J
env\\K N
)\\N O
{]] 	
if^^ 
(^^ 
env^^ 
.^^ 
IsDevelopment^^ !
(^^! "
)^^" #
)^^# $
{__ 
app`` 
.`` %
UseDeveloperExceptionPage`` -
(``- .
)``. /
;``/ 0
}aa 
elsebb 
{cc 
appdd 
.dd 
UseExceptionHandlerdd '
(dd' (
$strdd( 5
)dd5 6
;dd6 7
appee 
.ee 
UseHstsee 
(ee 
)ee 
;ee 
}ff 
apphh 
.hh 
UseCorshh 
(hh 
opthh 
=>hh 
{ii 
optjj 
.jj 
AllowAnyOriginjj "
(jj" #
)jj# $
;jj$ %
optkk 
.kk 
AllowAnyHeaderkk "
(kk" #
)kk# $
;kk$ %
optll 
.ll 
AllowAnyMethodll "
(ll" #
)ll# $
;ll$ %
}mm 
)mm 
;mm 
appoo 
.oo 
UseStaticFilesoo 
(oo 
)oo  
;oo  !
apppp 
.pp 
UseCookiePolicypp 
(pp  
)pp  !
;pp! "
appqq 
.qq 

UseSignalRqq 
(qq 
routeqq  
=>qq! #
{rr 
routess 
.ss 
MapHubss 
<ss 
	PortalHubss &
>ss& '
(ss' (
$strss( 4
)ss4 5
;ss5 6
}tt 
)tt 
;tt 
appvv 
.vv 
UseAuthenticationvv !
(vv! "
)vv" #
;vv# $
appww 
.ww 

UseSwaggerww 
(ww 
)ww 
;ww 
appxx 
.xx 
UseSwaggerUIxx 
(xx 
cxx 
=>xx !
{yy 
czz 
.zz 
SwaggerEndpointzz !
(zz! "
$strzz" <
,zz< =
$strzz> J
)zzJ K
;zzK L
}{{ 
){{ 
;{{ 
app}} 
.}} 
UseMvc}} 
(}} 
routes}} 
=>}}  
{~~ 
routes 
. 
MapRoute 
(  
name
�� 
:
�� 
$str
�� #
,
��# $
template
�� 
:
�� 
$str
�� F
)
��F G
;
��G H
routes
�� 
.
�� 
MapRoute
�� 
(
��  
name
�� 
:
�� 
$str
�� %
,
��% &
template
�� 
:
�� 
$str
�� )
+
��* +

MixService
��, 6
.
��6 7
	GetConfig
��7 @
<
��@ A
string
��A G
>
��G H
(
��H I
MixConstants
��I U
.
��U V"
ConfigurationKeyword
��V j
.
��j k
DefaultCulture
��k y
)
��y z
+
��{ |
$str��} �
)��� �
;��� �
routes
�� 
.
�� 
MapRoute
�� 
(
��  
name
�� 
:
�� 
$str
�� !
,
��! "
template
�� 
:
�� 
$str
�� )
+
��* +

MixService
��, 6
.
��6 7
	GetConfig
��7 @
<
��@ A
string
��A G
>
��G H
(
��H I
MixConstants
��I U
.
��U V"
ConfigurationKeyword
��V j
.
��j k
DefaultCulture
��k y
)
��y z
+
��{ |
$str��} �
)��� �
;��� �
routes
�� 
.
�� 
MapRoute
�� 
(
��  
name
�� 
:
�� 
$str
�� 
,
��  
template
�� 
:
�� 
$str
�� (
+
��) *

MixService
��+ 5
.
��5 6
	GetConfig
��6 ?
<
��? @
string
��@ F
>
��F G
(
��G H
MixConstants
��H T
.
��T U"
ConfigurationKeyword
��U i
.
��i j
DefaultCulture
��j x
)
��x y
+
��z {
$str��| �
)��� �
;��� �
routes
�� 
.
�� 
MapRoute
�� 
(
��  
name
�� 
:
�� 
$str
��  
,
��  !
template
�� 
:
�� 
$str
�� )
+
��* +

MixService
��, 6
.
��6 7
	GetConfig
��7 @
<
��@ A
string
��A G
>
��G H
(
��H I
MixConstants
��I U
.
��U V"
ConfigurationKeyword
��V j
.
��j k
DefaultCulture
��k y
)
��y z
+
��{ |
$str��} �
)��� �
;��� �
routes
�� 
.
�� 
MapRoute
�� 
(
��  
name
�� 
:
�� 
$str
�� #
,
��# $
template
�� 
:
�� 
$str
�� )
+
��* +

MixService
��, 6
.
��6 7
	GetConfig
��7 @
<
��@ A
string
��A G
>
��G H
(
��H I
MixConstants
��I U
.
��U V"
ConfigurationKeyword
��V j
.
��j k
DefaultCulture
��k y
)
��y z
+
��{ |
$str��} �
)��� �
;��� �
routes
�� 
.
�� 
MapRoute
�� 
(
��  
name
�� 
:
�� 
$str
�� #
,
��# $
template
�� 
:
�� 
$str
�� *
+
��+ ,

MixService
��- 7
.
��7 8
	GetConfig
��8 A
<
��A B
string
��B H
>
��H I
(
��I J
MixConstants
��J V
.
��V W"
ConfigurationKeyword
��W k
.
��k l
DefaultCulture
��l z
)
��z {
+
��| }
$str��~ �
)��� �
;��� �
}
�� 
)
�� 
;
�� 
}
�� 	
}
�� 
}�� 