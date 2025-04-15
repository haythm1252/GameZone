$(document).ready(function () {
    $('.deleted-alert').on('click', function () {
        var btn = $(this);
        const swal = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger",
                cancelButton: "btn btn-success"
            },
            buttonsStyling: false
        });
        swal.fire({
            title: "Are you sure you want to delete this game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: 'Games/Delete/' + btn.data('id'),
                    method: 'DELETE',
                    success: function () {
                        swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire({
                            title: "OOOPS",
                            text: "Something went wrong.",
                            icon :"error"
                        });
                    }
                });


            } 
        });        
    });
});
