﻿<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="14.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props" Condition="Exists('..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')" />
  <Import Project="..\packages\Microsoft.Net.Compilers.1.0.0\build\Microsoft.Net.Compilers.props" Condition="Exists('..\packages\Microsoft.Net.Compilers.1.0.0\build\Microsoft.Net.Compilers.props')" />
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{EF8640B4-ADA8-4613-B7E4-446646E0920D}</ProjectGuid>
    <ProjectTypeGuids>{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>DSchedule.Web</RootNamespace>
    <AssemblyName>DSchedule.Web</AssemblyName>
    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>
    <MvcBuildViews>false</MvcBuildViews>
    <UseIISExpress>true</UseIISExpress>
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
    <UseGlobalApplicationHostFile />
    <NuGetPackageImportStamp>
    </NuGetPackageImportStamp>
    <SccProjectName>SAK</SccProjectName>
    <SccLocalPath>SAK</SccLocalPath>
    <SccAuxPath>SAK</SccAuxPath>
    <SccProvider>SAK</SccProvider>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="EntityFramework.SqlServer">
      <HintPath>..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.SqlServer.dll</HintPath>
    </Reference>
    <Reference Include="GridMvc, Version=2.0.0.0, Culture=neutral, processorArchitecture=MSIL">
      <HintPath>..\packages\Grid.Mvc.3.0.0\lib\net40\GridMvc.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.AI.Agent.Intercept, Version=1.2.0.1011, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.Agent.Intercept.1.2.0\lib\net45\Microsoft.AI.Agent.Intercept.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.AI.DependencyCollector, Version=1.2.3.246, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.DependencyCollector.1.2.3\lib\net45\Microsoft.AI.DependencyCollector.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.AI.PerfCounterCollector, Version=1.2.3.246, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.PerfCounterCollector.1.2.3\lib\net45\Microsoft.AI.PerfCounterCollector.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.AI.ServerTelemetryChannel, Version=1.2.3.246, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel.1.2.3\lib\net45\Microsoft.AI.ServerTelemetryChannel.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.AI.Web, Version=1.2.3.246, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.Web.1.2.3\lib\net45\Microsoft.AI.Web.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.AI.WindowsServer, Version=1.2.3.246, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.WindowsServer.1.2.3\lib\net45\Microsoft.AI.WindowsServer.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.ApplicationInsights, Version=1.2.3.490, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.ApplicationInsights.1.2.3\lib\net45\Microsoft.ApplicationInsights.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <HintPath>..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\lib\net45\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="Newtonsoft.Json, Version=9.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL">
      <HintPath>..\packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll</HintPath>
      <Private>True</Private>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Data" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Runtime.Serialization" />
    <Reference Include="System.Security" />
    <Reference Include="System.Web.DynamicData" />
    <Reference Include="System.Web.Entity" />
    <Reference Include="System.Web.ApplicationServices" />
    <Reference Include="System.ComponentModel.DataAnnotations" />
    <Reference Include="System.Core" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Web" />
    <Reference Include="System.Web.Extensions" />
    <Reference Include="System.Web.Abstractions" />
    <Reference Include="System.Web.Routing" />
    <Reference Include="System.Xml" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.Web.Services" />
    <Reference Include="System.EnterpriseServices" />
    <Reference Include="Microsoft.Web.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll</HintPath>
    </Reference>
    <Reference Include="System.Net.Http">
    </Reference>
    <Reference Include="System.Net.Http.WebRequest">
    </Reference>
    <Reference Include="System.Web.Helpers, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.Helpers.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.AspNet.Mvc.5.2.3\lib\net45\System.Web.Mvc.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Optimization">
      <HintPath>..\packages\Microsoft.AspNet.Web.Optimization.1.1.3\lib\net40\System.Web.Optimization.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.AspNet.Razor.3.2.3\lib\net45\System.Web.Razor.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages.Deployment, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Deployment.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>..\packages\Microsoft.AspNet.WebPages.3.2.3\lib\net45\System.Web.WebPages.Razor.dll</HintPath>
    </Reference>
    <Reference Include="WebGrease">
      <Private>True</Private>
      <HintPath>..\packages\WebGrease.1.5.2\lib\WebGrease.dll</HintPath>
    </Reference>
    <Reference Include="Antlr3.Runtime">
      <Private>True</Private>
      <HintPath>..\packages\Antlr.3.4.1.9004\lib\Antlr3.Runtime.dll</HintPath>
    </Reference>
  </ItemGroup>
  <ItemGroup>
    <Compile Include="App_Start\BundleConfig.cs" />
    <Compile Include="App_Start\FilterConfig.cs" />
    <Compile Include="App_Start\RouteConfig.cs" />
    <Compile Include="Controllers\HomeController.cs" />
    <Compile Include="Controllers\JobDesignController.cs" />
    <Compile Include="Controllers\JobMonitoringController.cs" />
    <Compile Include="Controllers\ReportsController.cs" />
    <Compile Include="Controllers\UserAccountsController.cs" />
    <Compile Include="Global.asax.cs">
      <DependentUpon>Global.asax</DependentUpon>
    </Compile>
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="Content\bootstrap.css" />
    <Content Include="Content\bootstrap.min.css" />
    <Content Include="Content\Gridmvc.css" />
    <Content Include="favicon.ico" />
    <Content Include="fonts\glyphicons-halflings-regular.svg" />
    <Content Include="Global.asax" />
    <Content Include="Content\Site.css" />
    <Content Include="Scripts\ai.0.15.0-build58334.js" />
    <Content Include="Scripts\ai.0.15.0-build58334.min.js" />
    <Content Include="Scripts\bootstrap.js" />
    <Content Include="Scripts\bootstrap.min.js" />
    <Content Include="ApplicationInsights.config">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Include="Grid.mvc.readme" />
    <None Include="Properties\PublishProfiles\DSchedular.pubxml" />
    <None Include="Scripts\jquery-1.10.2.intellisense.js" />
    <Content Include="Scripts\Environment.js" />
    <Content Include="Scripts\gridmvc.js" />
    <Content Include="Scripts\gridmvc.lang.ru.js" />
    <Content Include="Scripts\gridmvc.min.js" />
    <Content Include="Scripts\jquery-1.10.2.js" />
    <Content Include="Scripts\jquery-1.10.2.min.js" />
    <None Include="Scripts\jquery.validate-vsdoc.js" />
    <Content Include="Scripts\jquery.validate.js" />
    <Content Include="Scripts\jquery.validate.min.js" />
    <Content Include="Scripts\jquery.validate.unobtrusive.js" />
    <Content Include="Scripts\jquery.validate.unobtrusive.min.js" />
    <Content Include="Scripts\modernizr-2.6.2.js" />
    <Content Include="Scripts\Node.js" />
    <Content Include="Scripts\respond.js" />
    <Content Include="Scripts\respond.min.js" />
    <Content Include="Scripts\Sessions.js" />
    <Content Include="Scripts\Uprocs.js" />
    <Content Include="Scripts\_references.js" />
    <Content Include="Web.config">
      <SubType>Designer</SubType>
    </Content>
    <Content Include="Web.Debug.config">
      <DependentUpon>Web.config</DependentUpon>
    </Content>
    <Content Include="Web.Release.config">
      <DependentUpon>Web.config</DependentUpon>
    </Content>
    <Content Include="Views\Web.config" />
    <Content Include="Views\_ViewStart.cshtml" />
    <Content Include="Views\Shared\Error.cshtml" />
    <Content Include="Views\Shared\_Layout.cshtml" />
    <Content Include="Views\Home\Error.cshtml" />
    <Content Include="Views\Home\Contact.cshtml" />
    <Content Include="Views\Home\Index.cshtml" />
    <Content Include="Views\JobDesign\NodesView.cshtml" />
    <Content Include="Views\Shared\_GridPager.cshtml" />
    <Content Include="Views\Shared\_Grid.cshtml" />
    <Content Include="Views\JobDesign\EnvironmentView.cshtml" />
    <Content Include="Views\JobDesign\UprocsView.cshtml" />
    <Content Include="Views\JobDesign\SessionsView.cshtml" />
    <Content Include="Views\Home\Login.cshtml" />
    <Content Include="Views\UserAccounts\UserDetails.cshtml" />
  </ItemGroup>
  <ItemGroup>
    <Folder Include="App_Data\" />
    <Folder Include="Models\" />
    <Folder Include="Views\JobMonitoring\" />
    <Folder Include="Views\Reports\" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="fonts\glyphicons-halflings-regular.woff" />
    <Content Include="fonts\glyphicons-halflings-regular.ttf" />
    <Content Include="fonts\glyphicons-halflings-regular.eot" />
    <Content Include="packages.config" />
    <None Include="Project_Readme.html" />
    <Content Include="Scripts\jquery-1.10.2.min.map" />
  </ItemGroup>
  <ItemGroup>
    <Service Include="{508349B6-6B84-4DF5-91F0-309BEEBAD82D}" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\DSchedule.BusinessComponent\DSchedule.BusinessComponent.csproj">
      <Project>{6212140b-af49-4586-836a-e772ac929da6}</Project>
      <Name>DSchedule.BusinessComponent</Name>
    </ProjectReference>
    <ProjectReference Include="..\DSchedule.Contracts\DSchedule.Contracts.csproj">
      <Project>{59188602-7006-4338-9511-9468D4A4BB04}</Project>
      <Name>DSchedule.Contracts</Name>
    </ProjectReference>
    <ProjectReference Include="..\DSchedule.Data\DSchedule.Data.csproj">
      <Project>{950DF4C9-F05A-47A6-9E31-AC880CF60653}</Project>
      <Name>DSchedule.Data</Name>
    </ProjectReference>
  </ItemGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">10.0</VisualStudioVersion>
    <VSToolsPath Condition="'$(VSToolsPath)' == ''">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>
  <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\WebApplications\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
  <Import Project="$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\WebApplications\Microsoft.WebApplication.targets" Condition="false" />
  <Target Name="MvcBuildViews" AfterTargets="AfterBuild" Condition="'$(MvcBuildViews)'=='true'">
    <AspNetCompiler VirtualPath="temp" PhysicalPath="$(WebProjectOutputDir)" />
  </Target>
  <ProjectExtensions>
    <VisualStudio>
      <FlavorProperties GUID="{349c5851-65df-11da-9384-00065b846f21}">
        <WebProjectProperties>
          <UseIIS>True</UseIIS>
          <AutoAssignPort>True</AutoAssignPort>
          <DevelopmentServerPort>56263</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl>http://localhost:56263/</IISUrl>
          <NTLMAuthentication>False</NTLMAuthentication>
          <UseCustomServer>False</UseCustomServer>
          <CustomServerUrl>
          </CustomServerUrl>
          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>
        </WebProjectProperties>
      </FlavorProperties>
    </VisualStudio>
  </ProjectExtensions>
  <Target Name="EnsureNuGetPackageBuildImports" BeforeTargets="PrepareForBuild">
    <PropertyGroup>
      <ErrorText>This project references NuGet package(s) that are missing on this computer. Use NuGet Package Restore to download them.  For more information, see http://go.microsoft.com/fwlink/?LinkID=322105. The missing file is {0}.</ErrorText>
    </PropertyGroup>
    <Error Condition="!Exists('..\packages\Microsoft.Net.Compilers.1.0.0\build\Microsoft.Net.Compilers.props')" Text="$([System.String]::Format('$(ErrorText)', '..\packages\Microsoft.Net.Compilers.1.0.0\build\Microsoft.Net.Compilers.props'))" />
    <Error Condition="!Exists('..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')" Text="$([System.String]::Format('$(ErrorText)', '..\packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.1.0.0\build\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props'))" />
  </Target>
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it.
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target> -->
</Project>