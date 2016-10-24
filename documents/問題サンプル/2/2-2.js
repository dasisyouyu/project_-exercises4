// reactチュートリアル２−２
var CommentBox = React.createClass({
  render: function() {
    return (
      <div className="commentBox">
        <h1>Comments</h1>
        <CommentList />{/*ここに先程の内容を展開します。*/}
        <CommentForm />{/*ここに先程の内容を展開します。*/}
      </div>
    );
  }
});