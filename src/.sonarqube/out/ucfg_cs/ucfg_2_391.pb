
�Mix.Cms.Lib.ViewModels.MixProducts.ReadListItemViewModel.GetModelListByModule(int, string, string, int, int?, int?, Mix.Cms.Lib.Models.Cms.MixCmsContext, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction)i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(	ModuleIdspecificultureorderByPropertyName	directionpageSize	pageIndex_context_transaction"0*
0*
1
2*�
1��
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�0 �(C
%0"$Mix.Cms.Lib.Models.Cms.MixCmsContext�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�4 �(A
%1"4Mix.Cms.Lib.Models.Cms.MixCmsContext.MixCmsContext()*

%0*
2*�
2�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(C	
context"__id*
""*
3
4*�
3�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�. �(>
%2"4Microsoft.EntityFrameworkCore.DbContext.Database.get*
	
context�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�. �(Q
%3"NMicrosoft.EntityFrameworkCore.Infrastructure.DatabaseFacade.BeginTransaction()*

%2*
4*�
4�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(Q
transaction"__id*
""*	
5
6
7*�
6�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(4
%4"9Mix.Cms.Lib.Models.Cms.MixCmsContext.MixModuleProduct.get*
	
context�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(Q
%5"�Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include<TEntity, TProperty>(System.Linq.IQueryable<TEntity>, System.Linq.Expressions.Expression<System.Func<TEntity, TProperty>>)*D"B
@Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions*

%4*
""�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(t
%6"�System.Linq.Queryable.Where<TSource>(System.Linq.IQueryable<TSource>, System.Linq.Expressions.Expression<System.Func<TSource, bool>>)*"
System.Linq.Queryable*

%5*
""�
�
j
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(�
%7"�System.Linq.Queryable.Select<TSource, TResult>(System.Linq.IQueryable<TSource>, System.Linq.Expressions.Expression<System.Func<TSource, TResult>>)*"
System.Linq.Queryable*

%6*
""�
�
j
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(�
query"__id*

%7�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�@ �(J
%8"__id*W*UE
CMix.Domain.Data.ViewModels.ViewModelBase<TDbContext, TModel, TView>"

Repository�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�@ �(
%9"�Mix.Domain.Data.Repository.ViewRepositoryBase<TDbContext, TModel, TView>.ParsePagingQuery(System.Linq.IQueryable<TModel>, string, int, int?, int?, TDbContext, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction)*

%8*	

query*

orderByPropertyName*
""*


pageSize*

	pageIndex*
	
context*

transaction�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs�7 �(
result"__id*

%9��
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(
%10"6Mix.Domain.Core.ViewModels.RepositoryResponse<TResult>�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(U
%11"KMix.Domain.Core.ViewModels.RepositoryResponse<TResult>.RepositoryResponse()*

%10�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �($
%12"DMix.Domain.Core.ViewModels.RepositoryResponse<TResult>.IsSucceed.set*

%10*
""�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(!
%13"?Mix.Domain.Core.ViewModels.RepositoryResponse<TResult>.Data.set*

%10*


result"t
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(

%10*
8*	
5
9
7*�
5�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(
%14"__id*W*UE
CMix.Domain.Data.ViewModels.ViewModelBase<TDbContext, TModel, TView>"

Repository�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(.
%15"jMix.Domain.Data.Repository.ViewRepositoryBase<TDbContext, TModel, TView>.LogErrorMessage(System.Exception)*

%14*

ex�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �((
%16""object.operator ==(object, object)*
"
object*

_transaction*
""*
10
11*�
10�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(*
%17"FMicrosoft.EntityFrameworkCore.Storage.IDbContextTransaction.Rollback()*

transaction*
11*�
11��
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(
%18"6Mix.Domain.Core.ViewModels.RepositoryResponse<TResult>�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(U
%19"KMix.Domain.Core.ViewModels.RepositoryResponse<TResult>.RepositoryResponse()*

%18�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(%
%20"DMix.Domain.Core.ViewModels.RepositoryResponse<TResult>.IsSucceed.set*

%18*
""�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(
%21"?Mix.Domain.Core.ViewModels.RepositoryResponse<TResult>.Data.set*

%18*
""�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �("
%22"DMix.Domain.Core.ViewModels.RepositoryResponse<TResult>.Exception.set*

%18*

ex"t
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(

%18*�
7�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �($
%23""object.operator ==(object, object)*
"
object*


_context*
""*
12
13*�
12�
�
i
]C:\_workspace\github\mixcore\mix.core\src\Mix.Cms.Lib\ViewModels\MixProducts\ReadViewModel.cs� �(%
%24"1Microsoft.EntityFrameworkCore.DbContext.Dispose()*
	
context*
13*
9*
14
13*

14*
13*
13"
""