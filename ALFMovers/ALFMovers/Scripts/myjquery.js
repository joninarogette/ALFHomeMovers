$(document).ready(function(){
    $("#reset").click(function(){
        $('#Customer').val("");
        $('#Contact').val("");
        $('#Address').val("");
        $('#Destination').val("");
        $('#sched').val("");
        });

    $("#clear").click(function(){
        $('#FName').val("");
        $('#LName').val("");
        $('#Contact').val("");
    });

    $("#void").click(function(){
        $('#plate').val("");
        $('#model').val("");
        $('#capacity').val("");
        $('#date').val("");
    });
    
    $("#submitsched").click(function () {

        $.ajax({
            type: 'get',
            url: '/Transaction/Create/' + test,
            success: function (data) {
                alert(data);
            }
        })

        $.each($("input[name='employee']:checked"),function(){
            var test = $(this).val();
            $.ajax({
                type: 'get',
                url: '/Employee/SchedSubmit/'+test,
                success: function (data) {
                    alert(data);
                }
            })
        })
        $.each($("input[name='truck']:checked"), function () {
            var test = $(this).val();
            $.ajax({
                type: 'get',
                url: '/Truck/SchedSubmit/' + test,
                success: function (data) {
                    alert(data);
                }
            })
        })
        location.reload();
    });

    $("#submittruck").click(function () {
        $.each($("input[name='truck']:checked"), function () {
            var test = $(this).val();
            $.ajax({
                type: 'get',
                url: '/Truck/SchedSubmit/' + test,
                success: function (data) {
                    alert(data);
                }
            })
        })
        location.reload();
    });

});



