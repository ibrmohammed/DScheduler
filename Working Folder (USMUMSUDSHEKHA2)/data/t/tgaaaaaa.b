﻿
@{
    ViewBag.Title = "DSchedular - Uprocs";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>Dschedule Uprocs</h2>
<html>
<head>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    <link href="@Url.Content("~/Content/Gridmvc.css")" rel="stylesheet" />
    <link href="@Url.Content("~/Content/bootstrap.min.css")" rel="stylesheet" />
    <script src="@Url.Content("~/Scripts/gridmvc.min.js")"></script>
    <script src="@Url.Content("~/Scripts/Uprocs.js")"></script>
</head>
<body>
    <div id="result" style="display:none;"></div>
    <div style="height:280px; width:100%">
        @model DSchedule.Contracts.Models.UprocsModel
        @using DSchedule.Contracts.Models
        @using GridMvc.Html
        @Html.Grid((List<DSchedule.Contracts.Models.Uprocs>)Model.UprocsList).Named("ordersGrid").Columns(columns =>
   {
       columns.Add(c => c.UprocID, true);
       columns.Add(c => c.UprocName).Titled("Uprocs").Filterable(false);
       columns.Add(c => c.JobTypeName).Titled("Type").Filterable(true);
       columns.Add(c => c.JobTypeID, true);
       columns.Add(c => c.EnvironmentName).Titled("Environment").Filterable(true);
       columns.Add(c => c.EnvironmentID, true);
       columns.Add(c => c.AccountName).Titled("User").Filterable(true);
       columns.Add(c => c.AccountTypeID, true);
       columns.Add(c => c.FolderPath).Titled("Folder Path").Filterable(true);
       columns.Add(c => c.Command).Titled("Command").Filterable(true);
       //columns.Add()
       //.Encoded(false)
       //.Sanitized(false)
       //.SetWidth(30)
       //.RenderValueAs(o => Html.Label(""));

   }).WithPaging(3).Sortable(true)
    </div>
    <div style="height:280px; width:100%" class="detailssection">

        <h3>Create Uproc</h3>
        @using (Html.BeginForm("UprocsView", "JobDesign", Model, FormMethod.Post))
        {
        <table style="height:300px; width:100%" class="detailssection">
            <tr>
                <td>
                    <table style="width:100%">
                        <tr>
                            <td width="12.5%" align="center">
                                @Html.HiddenFor(model => Model.UprocID, new { @id = "hdnUprocId" })
                                <div class="editor-label">
                                    @Html.Label("Uproc Name")
                                </div>
                            </td>
                            <td width="12.5%">
                                <div class="editor-field">
                                    @Html.TextBoxFor(model => Model.UprocName, new { @id = "txtUprocName", style = "width:13em; height:2em" })
                                    @Html.ValidationMessageFor(modelItem => @Model.UprocName)
                                </div>
                            </td>
                            <td width="12.5%" align="center">
                                <div class="editor-label">
                                    @Html.Label("Job Type")
                                </div>
                            </td>
                            <td width="12.5%" align="center">
                                <div class="editor-field">
                                    @Html.DropDownListFor(x => x.JobTypeID, new SelectList(ViewBag.JobRef, "RefId", "RefDescription"), new { @id = "cmbJobType", style = "width:13em; height:2em" })
                                    @Html.ValidationMessageFor(modelItem => @Model.JobTypeID)
                                </div>
                            </td>
                            <td width="12.5%" align="center">
                                <div class="editor-label">
                                    @Html.Label("Environment")
                                </div>
                            </td>
                            <td align="center">
                                <div class="editor-field">
                                    @Html.DropDownListFor(x => x.EnvironmentID, new SelectList(ViewBag.EnvironmentRef, "RefId", "RefDescription"), new { @id = "cmbEnvironment", style = "width:13em; height:2em" })
                                    @Html.ValidationMessageFor(modelItem => @Model.EnvironmentID)
                                </div>
                            </td>
                            <td width="12.5%" align="center">
                                <div class="editor-field">
                                    @Html.Label("Type Of Account")
                                </div>
                            </td>
                            <td width="12.5%" align="center">
                                <div class="editor-field">
                                    @Html.DropDownListFor(x => x.AccountTypeID, new SelectList(ViewBag.AccountRef, "RefId", "RefDescription"), new { @id = "cmbAccountType", style = "width:13em; height:2em" })
                                    @Html.ValidationMessageFor(modelItem => @Model.AccountTypeID)
                                </div>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td width="12.5%" align="center">
                                <div class="editor-label">
                                    @Html.Label("Folder Path")
                                </div>
                            </td>
                            <td colspan="5" align="left">
                                <div class="editor-field">
                                    @Html.TextBoxFor(m => m.FolderPath, new { type = "file", @id = "txtFolderPath", style = "height:2em" })
                                    @Html.ValidationMessageFor(modelItem => @Model.FolderPath)
                                </div>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td align="center">
                                <div class="editor-label">
                                    @Html.Label("Command")
                                </div>
                            </td>
                            <td colspan="5">
                                <div class="editor-field">
                                    @Html.TextBoxFor(model => Model.Command, new { id = "txtCommand", style = "width:13em; height:2em" })
                                    @Html.ValidationMessageFor(modelItem => @Model.Command)
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:100px"></tr>
            <tr>
                <td>
                    <table style="width:25%">
                        <tr>
                            <td align="center">

                                <input type="button" value="New Uproc" name="btnNewNode" />

                            </td>
                            <td align="center">

                                <input name="btnSave" type="submit" value="Save" />

                            </td>
                            <td align="center">

                                <input id="btnCancel" type="button" value="Cancel" />

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        }
    </div>

</body>
</html>
