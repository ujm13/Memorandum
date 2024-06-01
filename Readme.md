# Memorandum
> 此side project使用三層式架構，共分兩個Controller:會員系統、代辦事項操作，使用docker起MS SQL做儲存，並將project打包成docker Image
## 功能
### 會員:
- 註冊會員
- 會員登入
### 代辦事項操作:
- 新增代辦事項
- 修改代辦事項
- 刪除代辦事項
- 查詢代辦事項明細
- 查詢所有代辦事項

## 使用套件
### 專案:
- Asp.Versioning.Mvc.ApiExplorer -控制Api版本
- FluentValidation.AspNetCore-自訂參數驗證，限制會員註冊填入資料格式
- Mapster-參數轉換
- Dapper-對SQL做操作
- Microsoft.Extensions.Options-透過Option Pattern注入並取得連線字串
### 單元測試:
- FluentAssertions-單元測試做結果驗證
- NSubstitute-單元測試模擬相依物件

## 使用框架
|.Net  Version|Framework|
|----|----|
|.Net 8.0|Asp.Net Core Web API|
|.Net 8.0|XUnit|

## Docker

**Build Image**
```shell
docker build -f  Memorandum.WebApplication/Dockerfile -t memorandum:0.0.1 .
```

**Docker run**
```shell
docker run -d -p 5129:8080 --name memorandum -e ASPNETCORE_ENVIRONMENT=Development -e Db_Connection="Password=Hm06240624;Persist Security Info=True;User ID=sa;Initial Catalog=Member;Data Source=IP,1433;TrustServerCertificate=true;" memorandum:0.0.1
```