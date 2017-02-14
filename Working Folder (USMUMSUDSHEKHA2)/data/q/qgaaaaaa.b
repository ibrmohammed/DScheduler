
@{
    ViewBag.Title = "EnvironmentView";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>DSchedule Environment</h2>
<html>
<head>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    <link href="@Url.Content("~/Content/Gridmvc.css")" rel="stylesheet" />
    <link href="@Url.Content("~/Content/bootstrap.min.css")" rel="stylesheet" />
    <script src="@Url.Content("~/Scripts/gridmvc.min.js")"></script>
    @*<script language="javascript">
            function CallMyFunc() {
                $('#newnode').val(null);
            }
        </script>*@
    <script src="@Url.Content("~/Scripts/Environment.js")"></script>
</head>
<body>
    <div style="height:350px; width:100%">
        @model DSchedule.Contracts.Models.EnvironmentModel

        @using GridMvc.Html
        @*@{
                ViewBag.Title = "Details";
            }
            @{
                Layout = null;
            }*@

        @Html.Grid((List<DSchedule.Contracts.Models.Environment>)Model.EnvironmentList).Named("environmentGrid").Columns(columns =>
   {
       columns.Add(c => c.EnvironmentID, true).Titled("Environment ID").Filterable(false).Css("style: Hide");
       columns.Add(c => c.EnvironmentName).Titled("Environment").Filterable(true);
       columns.Add(c => c.NodeName).Titled("Node").Filterable(true);
       columns.Add(c => c.NodeID, true).Titled("Node ID").Filterable(false).Css("style: Hide");
       columns.Add(c => c.CreatedDateTime).Titled("Added Date").Filterable(true);
       columns.Add(c => c.EnvironmentPath).Titled("Path").Filterable(true);
       //columns.Add()
       //.Encoded(false)
       //.Sanitized(false)
       //.SetWidth(30)
       //.RenderValueAs(o => Html.Label("Edit")).Css("text-decoration: underline");
       //"Edit", "Edit", new { id = o.EnvironmentID }));

   }).WithPaging(7).Sortable(true)
    </div>
    <div style="height: 40px; width:100%"></div>
    <div style="height:245px; width:100%" class="detailssection">
        <h3>Create Environment</h3>
        @using (Html.BeginForm("SaveEnvironment", "JobDesign", FormMethod.Post, new { transaction = Model }))
        {
            <table style="height:210px; width:100%">
                <tr>
                    <td>
                        <table style="width:80%">
                            <tr>
                                <td align="center" width="20%">
                                    @Html.Label("Environment")
                                </td>
                                <td align="left" width="10%" style="color:black;">
                                    @Html.HiddenFor(model => Model.EnvironmentID, new { @id = "EnvironmentID" })
                                    @Html.TextBoxFor(model => Model.EnvironmentName, new { @id = "EnvironmentName" })
                                </td>
                                <td align="center" width="20%">
                                    @Html.Label("Node")
                                </td>
                                <td align="left" width="10%" style="color:black;">
                                    @{
                                        List<SelectListItem> listItems = new List<SelectListItem>();
                                        Model.NodesList.ForEach(x => listItems.Add(new SelectListItem { Text = x.NodeName, Value = x.NodeID.ToString() }));
                                    }
                                    @Html.DropDownListFor(model => Model.NodeID, listItems, new { @id = "NodeID" })
                                </td>
                                <td width="10%"> &nbsp;</td>
                                <td align="center" width="30%">
                                    @Html.Label("Environment Path")
                                </td>
                                <td align="left" width="40%" style="color:black;">
                                    @Html.TextBoxFor(model => Model.EnvironmentPath, new { type = "File", @id = "EnvironmentPath" })
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                @*<tr style="height:100px"></tr>*@
                <tr>
                    <td>
                        <table style="width:25%">
                            <tr>
                                <td align="center">
                                    <input type="button" value="New Environment" name="btnNewNode" onclick="NewEnvironment()" />
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
