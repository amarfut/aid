class Post {
    constructor(id, title, postUrl, viewsCount, commentsCount, description, likesCount, dislikesCount) {
        this.id = id;
        this.title = title;
        this.viewsCount = viewsCount > 1000 ? Math.floor(viewsCount / 1000) + 'k' : viewsCount;
        this.commentsCount = commentsCount;
        this.description = description;
        this.likesCount = ko.observable(likesCount);
        this.dislikesCount = ko.observable(dislikesCount);
        this.imageUrl = 'https://storage.googleapis.com/youit/images/' + postUrl + '_mini.png';
        this.postUrl = '/post/' + postUrl;
    }
}

function AppViewModel() {
    let self = this;
    self.posts = ko.observableArray([]);
    self.morePostsButtonVisible = ko.observable(true);
    self.endPostsTextVisible = ko.observable(false);

    self.showComments = function (post) {
        return post.postUrl + '#begin-comments';
    };

    self.setReaction = function (postId, liked) {
        $.ajax({
            url: '/home/setpostreaction',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({ PostId: postId, Liked: liked }),
            success: (data) => {
                const post = self.posts().find(p => p.id === postId);
                post.likesCount(data.likes);
                post.dislikesCount(data.dislikes);
            },
            error: (error) => {
                if (error.status === 403) {
                    $('#authorizeModal').modal("show");
                }
            }
        });
    };

    self.addPosts = function () {
        let skip = 20 + self.posts().length;
        self.morePostsButtonVisible(false);

        $.get('/home/loadposts?skip=' + skip, function (posts) {
            let postsPresent = posts && posts.length > 0;
            self.morePostsButtonVisible(postsPresent);
            self.endPostsTextVisible(!postsPresent);
            if (!postsPresent) return;
            for (let post of posts) {
                self.posts.push(new Post(post.id, post.title, post.url, post.viewsCount, post.commentsCount, post.description, post.likesCount, post.dislikesCount));
            }
        });
    };
}
ko.applyBindings(new AppViewModel());