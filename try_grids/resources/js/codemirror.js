
$(document).ready(function () {
    //根据DOM元素的id构造出一个编辑器

})

var editor = CodeMirror.fromTextArea(document.getElementById("code"), {
    value: "function myScript(){return 100;}\n",
    mode:  "javascript",
    lineNumbers: true,	//显示行号
    theme: "dracula",	//设置主题
    // lineWrapping: true,	//代码折叠
    // foldGutter: true,
    // gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"],
    // matchBrackets: true,	//括号匹配
    // //readOnly: true,        //只读
});