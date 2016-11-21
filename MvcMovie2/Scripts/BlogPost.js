/*
$(".useremail")
    .on("click", GetBlogTitleList);
*/

function GetBlogTitleList(element) {

    $.ajax({
            url: "GetBlogTitleList",
            data: { usernameStat: element.dataset.id }
        })
        .done(function(html) {
            $("#titleDetail").html(html);
            AttachBlogTitleHandler();
        });
    return false;
}

function AttachBlogTitleHandler() {
    $(".BlogTitle")
        .on("click",
            function() {
                $.ajax({
                        url: "BlogPost",
                        data: { blogTitle: this.dataset.id }

                    })
                    .done(function(html) {
                        $("#detail").html(html);

                    });
                return false;
            });
}

