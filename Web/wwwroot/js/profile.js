function ProfileCommentsViewModel() {
    let self = this;
    self.comments = ko.observableArray([]);
    self.noCommentsVisible = ko.observable(false);

    self.getPostUrl = function (postUrl, commentId) {
        if (postUrl === 'dotnetweb') return `/map/${postUrl}#${commentId}`;
        return `/post/${postUrl}#${commentId}`;
    };

    self.load = function () {
        $.ajax({
            url: '/account/getprofilecomments',
            type: 'GET',
            success: (responseComments) => {
                self.noCommentsVisible(responseComments.length === 0);
                for (let comment of responseComments) {
                    self.comments.push(comment);
                }
            },
            error: (error) => {

            }
        });
    };

    self.delete = function (comment) {
        if (confirm("Are you sure?")) {
            const commentId = comment.commentId;
            const isTopLevel = comment.parentCommentId === null;
            $.ajax({
                url: '/account/deletecomment',
                type: 'POST',
                contentType: "application/json",
                data: JSON.stringify({
                    CommentId: commentId,
                    TopLevel: isTopLevel,
                    PostUrl: comment.postUrl
                }),
                success: (comments) => {
                    const comment = self.comments().find(p => p.commentId === commentId);
                    self.comments.remove(comment);
                },
                error: (error) => {

                }
            });
        }
    };

    self.load();
}


const profileComments = document.getElementById("profile-comments");
if (profileComments) {
    ko.applyBindings(new ProfileCommentsViewModel(), document.getElementById("profile-comments"));
}
