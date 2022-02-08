window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Operation Successful', { timeout:10000 });
    }
    if (type === "error") {
        toastr.error(message, 'Operation Failed', { timeout: 10000 });
    }
}
window.SweetAlert = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Operation Successful',
            message,
            'success'
        );
    }
    if (type === "error") {
        Swal.fire({
            icon: 'error',
            title: 'Operation Failed',
            text: message,
        });
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}


function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}