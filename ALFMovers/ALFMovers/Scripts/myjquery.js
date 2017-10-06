$(document).ready(function(){
    $("#reset").click(function(){
        $('#Customer').val("");
        $('#Contact').val("");
        $('#Address').val("");
        $('#Destination').val("");
        $('#datepicker').val("");
        });

    $("#void").click(function(){
        $('#plate').val("");
        $('#model').val("");
        $('#capacity').val("");
        $('#date').val("");
    });
    
    $("#submitsched").click(function () {
        alert("Transaction Saved");
        var amount = $('#payment').val();
        var transid = "";
        $.ajax({
            type: 'get',
            url: '/Transactions/newtrans/' + amount,
            success: function (data) {
                console.log("transaction" + data);
            }
        });
        $.each($("input[name='employee']:checked"), function () {
            var test = $(this).val();
            $.ajax({
                type: 'get',
                url: '/Employee/SchedSubmit/' + test,
                success: function (data) {
                    console.log("Employee" + data);
                }
            })
        });
        $.each($("input[name='truck']:checked"), function () {
            var test = $(this).val();
            $.ajax({
                type: 'get',
                url: '/Truck/SchedSubmit/' + test,
                success: function (data) {
                    console.log("Truck" + data);
                }
            })
        });
        
        window.location.href = '/Customer/List';

        
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
        
    });

});



