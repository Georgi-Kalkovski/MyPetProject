$(document).ready(function () {
    $('#subbreeds').DataTable({
        'columnDefs': [
            {
                'searchable': false,
                'targets': [4]
            },
        ]
    });
});