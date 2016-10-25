$(document).ready(function () {
    $.get("/Graph/Connections", function (data) {
        console.log(data);
        $.each(data, function (index, item) {
            drawConnection("node-" + item.nodes[0], "node-" + item.nodes[1], item.cid);
        });
    }, "json");
    
});

function drawConnection(source, dest, cid) {
    jsPlumb.connect({
        source: source,
        target: dest,
        anchor: "AutoDefault",
        overlays:[ 
                    ["Arrow" , { width:12, length:12, location:-5 }]
        ],
        paintStyle: { strokeStyle: "black", lineWidth: 2 },
        endpoint: "Blank",
        connector: "Straight",
        parameters: {"cid": cid}
    });
}
function get_next_connection_id() {
    return "tcid-" + $("svg").length;
}