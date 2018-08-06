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
});

$(".uploadPhoto").on("click", function (ev) {
    $("#imageUploadForm").click();
})
