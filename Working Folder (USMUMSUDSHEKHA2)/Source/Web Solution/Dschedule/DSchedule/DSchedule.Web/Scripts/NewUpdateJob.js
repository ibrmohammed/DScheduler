$(document).ready(function () {
    $(function () {
        pageGrids.environmentGrid.onRowSelect(function (e) {
            $('#JobRuleId').val(e.row.JobRuleId);
            $('#UprocID').val(e.row.UprocId);
            $('#SessionId').val(e.row.SessionId);
            $('#EnvironmentID').val(e.row.EnvironmentId);
        });
    });
});


function NewJob() {
    $('#JobRuleId').val(null);
    $('#UprocID').val(null);
    $('#SessionId').val(null);
    $('#EnvironmentID').val(null);
}