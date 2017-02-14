$(document).ready(function () {
    $(function () {
        pageGrids.nodeGrid.onRowSelect(function (e) {
            $('#NodeName').val(e.row.NodeName);
            $('#Path').val(e.row.NodePath);
            $('#NodeID').val(e.row.NodeID);
        });
    });
});


function NewNode() {
    $('#NodeName').val(null);
    $('#Path').val(null);
    $('#EnvironmentPath').val(null);
    $('#NodeID').val(null);
}