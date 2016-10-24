(function () {
    AddValidateForLoginForm();

    function AddValidateForLoginForm() {
        var form = $('#loginForm');

        $(form).validate({
            rules: {
                userName: {
                    required: true,
                    minlength: 4
                },
                password: {
                    required: true,
                    minlength: 6
                }
            },
            submitHandler: function () {
                $(form).find('#submit').prop('disabled', true);

                var user = {
                    UserName: $(form).find('#userName').val(),
                    Password: $(form).find('#password').val()
                }

                $.ajax({
                    url: '/User/Login',
                    type: 'Post',
                    dataType: 'json',
                    data: user
                }).done(function (data) {
                    if (data.IsResult != true) {
                        $('#message').text(data.ResultMessage);
                    } else {
                        if (data.MinUser != null) {
                            $('#message').text('ようこそ、' + data.MinUser.UserName + 'さん(未ログイン)');
                        }
                    }
                }).fail(function (data) {
                    console.log(data);
                    $('#message').text('通信エラーが発生しました。');
                }).always(function () {
                    $(form).find('#submit').prop('disabled', false);
                });
            }
        });
    }
})();