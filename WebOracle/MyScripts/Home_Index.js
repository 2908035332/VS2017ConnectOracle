var app = new Vue({
    el: "#app"
    , data: {
        items: null
        ,MyTest: {
            id: ""
            , name: ""
            , time: ""
        }
        //初始化
        ,MyTestClose: null
        ,name: ""
        ,StartTime: null
        ,EndTime: null
        ,
    }
    , methods: {
        //查询所有
        Load: function () {
            var _this = this;
            _this.$http.get("http://localhost:53375/api/MyTest").then(function (r) {
                _this.items = r.data;
            });
        }
        //删除
        , Delete: function (value) {
            if (!confirm("是否移除")) return;
            var _this = this;
            _this.$http.delete("http://localhost:53375/api/MyTest/" + value).then(function (r) {
                if (r) {
                    alert("已移除");
                    _this.Load();
                } else {
                    alert("移除失败");
                }
            });
        }
        //修改之（查询）
        , Update: function (value) {
            $("#myModal").unbind();//解除所有事件绑定
            $('#myModal').modal('show');
            $(".modal-title").text("修改信息");
            $("#Save").text("修改");
            var _this = this;
            _this.$http.get("http://localhost:53375/api/MyTest/" + value).then(function (r) {
                r.data.time = ToDateTime(r.data.time);
                _this.MyTest = r.data;
            });

        }
        //添加
        , Insert: function () {
            //清空
            this.MyTest = JSON.parse(JSON.stringify(this.MyTestClose));
            $("#myModal").unbind();//解除所有事件绑定
            $('#myModal').modal('show');
            $(".modal-title").text("添加信息");
            $("#Save").text("添加");
        }
        //保存_修改与保存添加
        , Save: function () {
            var _this = this;
            if (_this.MyTest.id == "") {
                _this.$http.post("http://localhost:53375/api/MyTest", _this.MyTest).then(function (r) {
                    if (r) {
                        alert("已添加");
                        $('#myModal').modal('hide');
                        _this.Load();
                    } else {
                        alert("无法添加");
                    }
                });
            } else {
                _this.$http.put("http://localhost:53375/api/MyTest", _this.MyTest).then(function (r) {
                    if (r) {
                        alert("已修改");
                        $('#myModal').modal('hide');
                        _this.Load();
                    } else {
                        alert("无法修改");
                    }
                });
            }
        }
        //条件查询
        , Select: function () {
            //console.log(this.StartTime + "----" + this.EndTime);
            var _this = this;
            _this.$http.get("http://localhost:53375/api/MyTest", {
                params: {
                    "name": _this.name
                    , "StartTime": _this.StartTime
                    , "EndTime": _this.EndTime
                }
            }).then(function (r) {
                _this.items = r.data;
            });
        }
        //重置多条件检索
        , Reset: function () {
            this.name = "";
            this.StartTime = null;
            this.EndTime = null;
            this.Load();
        }
        //vue时间格式化
        , dateFormat: function (time) {
            var date = new Date(time);
            var year = date.getFullYear();
            /* 在日期格式中，月份是从0开始的，因此要加0
             * 使用三元表达式在小于10的前面加0，以达到格式统一  如 09:11:05
             * */
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            //var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
            //var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
            //var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
            // 拼接
            return year + "-" + month + "-" + day;//+ " " + hours + ":" + minutes + ":" + seconds;
        }
    }
    , created: function () {
        var _this = this;
        //清空
        _this.MyTestClose = JSON.parse(JSON.stringify(this.MyTest));
        _this.Load();
    }

});
//时间转换
function ToDateTime(time) {
    var date = new Date(time);
    return date.getFullYear() + '-' + checkTime(date.getMonth() + 1) + "-" + checkTime(date.getDate());
}
function checkTime(i) {
    if (i < 10) {
        i = '0' + i
    }
    return i
}
/**
 * 显示倒计时
 * 结束时间@param {any} time
 */
function showTime(time) {
    var timerId = setInterval(function () {
        var end = Date.parse(time);
        var now = Date.now();
        // 计算差值
        var offset = Math.floor((end - now) / 1000);//毫秒
        if (offset <= 0) {
            clearInterval(timerId);
        }

        var sec = offset % 60;
        var min = Math.floor(offset / 60) % 60;
        var hour = Math.floor(offset / 60 / 60) % 24;
        var day = Math.floor(offset / 60 / 60 / 24);

        $("#MyTest").html(day + "天" + hour + "时" + min + "分" + sec + "秒");


    }, 1000);
}