(function () {
    AddValidateForCreateForm();

    function AddValidateForCreateForm() {
        var form = $('#createForm');

        $(form).validate({
            rules: {
                userName: {
                    required: true,
                    minlength: 4
                },
                password: {
                    required: true,
                    minlength: 6
                },
                mailAddress: {
                    required: true,
                    email: true
                }
            },
            submitHandler: function () {
                $(form).find('#submit').prop('disabled', true);

                var user = {
                    UserName: $(form).find('#userName').val(),
                    Password: $(form).find('#password').val(),
                    MailAddress: $(form).find('#mailAddress').val()
                }

                var requestVerificationToken = $(form).find('[name="__RequestVerificationToken"]').val();

                $.ajax({
                    url: '/User/Create',
                    type: 'Post',
                    dataType: 'json',
                    data: { user, __RequestVerificationToken: requestVerificationToken }
                }).done(function (data) {
                    if (data.IsResult != true) {
                        $('#message').html(data.ResultMessage);
                    } else {
                        alert('ようこそ、' + user.UserName + 'さん(未ログイン)');
                        window.location.href = "/Home";
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