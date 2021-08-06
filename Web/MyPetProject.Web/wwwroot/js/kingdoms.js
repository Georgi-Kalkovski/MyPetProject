$(document).ready(function () {
    $('#kingdoms').DataTable({
        'columnDefs': [
            {
                'searchable': false,
                'targets': [6]
            },
        ]
    });
});