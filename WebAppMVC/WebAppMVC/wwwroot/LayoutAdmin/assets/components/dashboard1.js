(function () {
    function initial() {
        loadDataChartOrder();
    }
    function loadDataChartOrder(){
        var chartDom = document.getElementById('id');
        var myChart = echarts.init(chartDom);
        var option;

        option = {
            title: {
                text: 'Thống kê số lượng bán theo tháng trong năm 2023',
                subtext: 'dữ liệu 2023',
                left: 'center'
            },
            xAxis: {
                type: 'category',
                data: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12']
            },
            yAxis: {
                type: 'value'
            },
            series: [
                {
                    data: [120, 200, 150, 80, 70, 110, 130, 80, 70, 110, 130, 120],
                    type: 'bar'
                }
            ]
        };

        option && myChart.setOption(option);

    }
    $(document).ready(function () {
        initial();
    });
    
})();
    