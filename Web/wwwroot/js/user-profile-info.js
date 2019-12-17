save = () => {
    $.ajax({
        url: '/save-profile',
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ userName: $('#userNameInput').val() }),
        success: () => {
            window.location.reload(true);
        }
    });
}