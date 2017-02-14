@{
    ViewBag.Title = "User Details";
    Layout = "~/Views/Shared/_Layout.cshtml";
}
@model DSchedule.Contracts.Models.UserProfileCollection
<h2>User Accounts</h2>

<html>
<head>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    <link href="@Url.Content("~/Content/Gridmvc.css")" rel="stylesheet" />
    <link href="@Url.Content("~/Content/bootstrap.min.css")" rel="stylesheet" />
    <script src="@Url.Content("~/Scripts/gridmvc.min.js")"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('.saveButton').click(function () {
                
        //        $("#result").dialog("open");
        //    });
        //    $("#result").dialog({
        //        autoOpen: false,
        //        title: 'Title',
        //        width: 500,
        //        height: 'auto',
        //        modal: true
        //    });
        //});        
    </script>
</head>
<body>
    <div id="result" style="display:none;"></div>
    <div style="height:350px; width:100%">


        @using GridMvc.Html

        @Html.Grid(Model.UserProfiles).Columns(columns =>
       {
           columns.Add(c => c.FirstName).Titled("First Name").Filterable(true);
           columns.Add(c => c.LastName).Titled("Last Name").Filterable(true);
           columns.Add(c => c.UserName).Titled("User Name").Filterable(true);
           columns.Add(c => c.Email).Titled("Email Id").Filterable(true);
           columns.Add(c => c.AccountType).Titled("Account Type").Filterable(true);
           columns.Add(c => c.IsActive).Titled("IsActive").Filterable(true);
           columns.Add()
           .Encoded(false)
           .Sanitized(false)
           .SetWidth(30)
           .RenderValueAs(o => Html.ActionLink("Edit", "EditUser", "UserAccountsController"
               , new { id = o.LoginID },new { @class = "saveButton", onclick = "return false;" }));
           


       }).WithPaging(7).Sortable(true)

    </div>
    <div style="height:280px; width:100%" class="detailssection">

        <h3>Create User</h3>
        @using (Html.BeginForm("CreateUser", "UserAccounts"))
        {
            @Html.AntiForgeryToken()
            @Html.ValidationSummary()
            <table>
                <tr>
                    <td>
                        @Html.LabelFor(model => Model.Default.UserName)
                    </td>
                    <td>
                        @Html.TextBoxFor(model => Model.Default.UserName, new { style = "width:10em;" })
                        @Html.ValidationMessageFor(model => Model.Default.UserName)
                    </td>
                    <td>
                        <div class="editor-label">
                            @Html.LabelFor(model => model.Default.FirstName)
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            @Html.TextBoxFor(model => model.Default.FirstName, new { style = "width:10em;" })
                            @Html.ValidationMessageFor(model => model.Default.FirstName)
                        </div>
                    </td>
                    <td>
                        <div class="editor-label">
                            @Html.LabelFor(model => model.Default.LastName)
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            @Html.TextBoxFor(model => model.Default.LastName, new { style = "width:10em;" })
                            @Html.ValidationMessageFor(model => model.Default.LastName)
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            @Html.LabelFor(model => model.Default.Email)
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            @Html.TextBoxFor(model => model.Default.Email, new { style = "width:10em;" })
                            @Html.ValidationMessageFor(model => model.Default.Email)
                        </div>
                    </td>

                    <td>
                        <div class="editor-label">
                            @Html.LabelFor(model => model.Default.Password)
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            @Html.PasswordFor(model => model.Default.Password, new { style = "width:10em;" })
                            @Html.ValidationMessageFor(model => model.Default.Password)
                        </div>
                    </td>
                    <td>
                        <div class="editor-label">
                            @Html.LabelFor(model => model.Default.ConfirmPassword)
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            @Html.PasswordFor(model => model.Default.ConfirmPassword, new { style = "width:10em;" })
                            @Html.ValidationMessageFor(model => model.Default.ConfirmPassword)
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                            <div class="editor-label">
                                @Html.LabelFor(model => model.Default.AccountType)
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                @Html.DropDownListFor(model => model.Default.AccountType,
                                        (SelectList)ViewBag.AccountTypes, new { style = "width:13em; height:2.5em" })
                            </div>
                        </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            <input type="submit" value="Create" />
                        </p>
                    </td>
                </tr>
            </table>
        }
    </div>
</body>
</html>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}