$(document).ready(function () {
    make_draggable($(".node"));
    $(document).on("click", "#graph-area", function (e) {
        if ($(e.target).is(".node"))
            return;
        var tid = get_next_node_id();
        var nnode = $("<div></div>", {
            id: tid,
            class: "node",
            style: "left: " + e.pageX + "px; " + "top: " + e.pageY + "px;" 
        });
        nnode.append(tid);
        $("#graph-area").append(nnode);
        make_draggable(nnode);
        $.post("/Graph/AddNode", {
            tid: tid,
            text: tid,
            x: e.pageX,
            y: e.pageY
        });
    });
    var selected_node = null;
    $(document).on("click", ".node", function () {
        if (selected_node == null) {
            selected_node = $(this);
            $("#node-input").show();
        } else {
            $("#node-input").hide();
            var source = selected_node.attr("id");
            var dest = $(this).attr("id");
            var cid = get_next_connection_id();
            drawConnection(source, dest, cid);
            $.post("/Graph/AddConnection", {
                tcid: cid,
                n1: source,
                n2: dest
            });
            selected_node = null;
        }
    });
    $("#node-input").on("submit", function (e) {
        if (selected_node != null) {
            e.preventDefault();
            var ntext = $("#ntext").val();
            selected_node.text(ntext);
            $.post("/Graph/UpdateNodeText", {
                nid: selected_node.attr("id"),
                text: ntext
            });
            $("#node-input").val("");
            $("#node-input").hide();
            selected_node = null;
        }
    });
    $(document).on("mousedown", ".node", function (e) {
        if (e.which != 3)
            return;
        var nid = $(this).attr("id");
        console.log("remove fired: " + nid);
        try {
            jsPlumb.remove(nid);
        } catch (err) {
            console.log("jsplumb is angry: " + err);
        }
        $(nid).remove();
        $.post("/Graph/DeleteNode", {
            tnid: nid
        });
    });
    $("#graph-area").on("contextmenu", function (e) {
        e.preventDefault();
    });
});
function get_next_node_id() {
    return "tnode-" + $(".node").length;
}
function make_draggable(select) {
    jsPlumb.draggable(select, {
        stop: function (e, ui) {
            var x = ui.position.left;
            var y = ui.position.top;
            var nid = $(this).attr("id");
            $.post("/Graph/UpdateNodeLocation", {
                nid: nid,
                x: x,
                y: y
            });
        }
    });
}