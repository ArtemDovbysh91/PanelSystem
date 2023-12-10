
dotnet restore Cint.CodingChallenge.sln -v q
if(!$LASTEXITCODE) { dotnet clean Cint.CodingChallenge.sln -v q }
if(!$LASTEXITCODE) { dotnet build Cint.CodingChallenge.sln -v q }
if(!$LASTEXITCODE) { cd .\src\Cint.CodingChallenge.Web\app\ ; npm install ; npm run build ; cd $PSScriptRoot }
if(!$LASTEXITCODE) { dotnet test --no-build test\Cint.CodingChallenge.Test\Cint.CodingChallenge.Test.csproj }
