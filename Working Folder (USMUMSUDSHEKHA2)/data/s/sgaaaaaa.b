
@{
    ViewBag.Title = "DSchedular - Sessions";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>DSchedule Sessions</h2>
<html>
<head>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    <link href="@Url.Content("~/Content/Gridmvc.css")" rel="stylesheet" />
    <link href="@Url.Content("~/Content/bootstrap.min.css")" rel="stylesheet" />
    <script src="@Url.Content("~/Scripts/gridmvc.min.js")"></script>
    <script src="@Url.Content("~/Scripts/Sessions.js")"></script>
</head>
<body>
    <div id="result" style="display:none;"></div>

    <div style="height:280px; width:100%">
        @model DSchedule.Contracts.Models.SessionsModel
        @using DSchedule.Contracts.Models
        @using GridMvc.Html
        @Html.Grid((List<DSchedule.Contracts.Models.Sessions>)Model.SessionsList).Named("ordersGrid").Columns(columns =>
   {
       columns.Add(c => c.SessionId, true);
       columns.Add(c => c.Session).Titled("Session").Filterable(false);
       columns.Add(c => c.Uproc).Titled("Uproc").Filterable(true);
       columns.Add(c => c.UprocId, true);
       columns.Add(c => c.Environment).Titled("Environment").Filterable(true);
       columns.Add(c => c.EnvironmentID, true);
       columns.Add(c => c.AccountTypeID,true);
       columns.Add(c => c.User).Titled("User").Filterable(true);
       //columns.Add()
       //.Encoded(false)
       //.Sanitized(false)
       //.SetWidth(30)
       //.RenderValueAs(o => Html.Label(""));

   }).WithPaging(3).Sortable(true)
    </div>
    <div style="height:200px; width:100%" class="detailssection">
        @using (Html.BeginForm("SessionsView", "JobDesign", Model, FormMethod.Post))
        {
            <h3>Create Session</h3>
            <table style="height:300px; width:100%" class="detailssection">
                <tr>
                    <td>
                        <table style="width:100%">
                            <tr>
                                <td width="12.5%" align="center">
                                    <div class="editor-label">
                                        @Html.HiddenFor(model => Model.SessionId, new { @id = "hdnSessionId" })
                                        @Html.Label("Session")
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-field">
                                        @Html.TextBoxFor(model => Model.SessionName, new { @id = "txtSessionName", style = "width:13em; height:2em" })
                                        @Html.ValidationMessageFor(modelItem => @Model.SessionName)
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-label">
                                        @Html.Label("Uprocs")
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-field">
                                        @Html.DropDownListFor(x => Model.UprocID, new SelectList(ViewBag.UprocsRef, "RefId", "RefDescription"), new { @id = "cmbUproc", style = "width:13em; height:2em" })
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-label">
                                        @Html.Label("Environment")
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-field">
                                        @Html.DropDownListFor(x => Model.EnvironmentID, new SelectList(ViewBag.EnvironmentRef, "RefId", "RefDescription"), new { @id = "cmbEnvironment", style = "width:13em; height:2em" })
                                        @Html.ValidationMessageFor(modelItem => @Model.EnvironmentID)
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-label">
                                        @Html.Label("User")
                                    </div>
                                </td>
                                <td width="12.5%" align="center">
                                    <div class="editor-field">
                                        @Html.DropDownListFor(x => Model.AccountTypeID, new SelectList(ViewBag.UserRef, "RefId", "RefDescription"), new { @id = "cmbAccount", style = "width:13em; height:2em" })
                                        @Html.ValidationMessageFor(modelItem => @Model.AccountTypeID)
                                    </div>
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
                            <tr>
                                <td colspan="8" align="left">
                                    <table class="table-dependencies">
                                        <tr style="min-height:30px;text-align:left;">
                                            <td colspan="6">
                                                <h4>Dependencies</h4><br />
                                            </td>
                                        </tr>
                                        <tr><td colspan="6"></td></tr>
                                        <tr style="min-height:30px;text-align:center;">
                                            <td>
                                                <div class="editor-label">
                                                    @Html.Label("Session")
                                                </div>
                                            </td>
                                            <td>
                                                <div class="editor-field">
                                                    @Html.DropDownListFor(x => Model.DependentSessionId, new SelectList(ViewBag.SessionsRef, "RefId", "RefDescription"), new { @id = "cmbDependentSession", style = "width:13em; height:2em" })
                                                </div>
                                            </td>
                                            <td>
                                                <div class="editor-label">
                                                    @Html.Label("Uproc")
                                                </div>
                                            </td>
                                            <td>
                                                <div class="editor-field">
                                                    @Html.DropDownListFor(x => Model.DependentUprocId, new SelectList(ViewBag.DependentUprocs, "RefId", "RefDescription"), new { @id = "cmbDependentUproc", style = "width:13em; height:2em" })
                                                </div>
                                            </td>
                                        </tr>
                                        <tr></tr>
                                    </table>
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

                                    <input type="button" value="New Session" name="btnNewNode" />

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
