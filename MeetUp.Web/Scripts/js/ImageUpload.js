$(document).ready(function () {
    $("#lightSlider").lightSlider(
        {
            item: 3,
            autoWidth: false,
            slideMove: 1, // slidemove will be 1 if loop is true
            slideMargin: 10,
        }
    );


    $("#imageUploadForm").change(function () {
        var formData = new FormData();
        var totalFiles = document.getElementById("imageUploadForm").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("imageUploadForm").files[i];
            formData.append("imageUploadForm", file);
        }
        $.ajax({
            type: "POST",
            url: '/Files/Upload',
            data: formData,
            contentType: false,
            processData: false,
            //success: function(response) {
            //    alert('succes!!');
            //},
            //error: function (error) {
            //    alert(error);
            //}
        }).done(function () {
            document.location.reload();

        }).fail(function (xhr, status, errorThrown) {
            alert('fail');
            console.log(status, errorThrown);
        });
    });


    $('.deleteItem').click(function (e) {
        e.preventDefault();
        var $ctrl = $(this);
        if (confirm('Do you really want to delete this file?')) {
            $.ajax({
                url: '/Files/Delete',
                type: 'POST',
                data: { id: $(this).data('id') }
            }).done(function (data) {
                if (data.Result == "OK") {
                    $ctrl.closest('li').remove();
                }
                else if (data.Result.Message) {
                    alert(data.Result.Message);
                }
            }).fail(function () {
                alert("There is something wrong. Please try again.");
            })

        }
    });

});

$(".uploadPhoto").on("click", function (ev) {
    $("#imageUploadForm").click();
})
