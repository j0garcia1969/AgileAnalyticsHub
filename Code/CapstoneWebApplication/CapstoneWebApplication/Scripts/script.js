function taskTypeChange(selected_task_type) {
    //alert(selected_task_type);
    if (selected_task_type != 0) {
        document.getElementById("TaskItem").setAttribute("style", "height:34px");
    }
    else {
        document.getElementById("TaskItem").setAttribute("style", "height:100px");
    }
}