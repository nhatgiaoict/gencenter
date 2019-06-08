function confirmDeleteFile(filePath, fileName){				
	if(confirm("Really delete '" + fileName + "'?"))
   	 	document.location.href = filePath;   	 			   	 			
}

function confirmDeleteFolder(filePath, folderName){				
	if(confirm("Really delete '" + folderName + "' and all it's subfiles and subfolders?")){
		if(parent.frames.length > 0)
   	 		parent.MenuPane.location.href = "MenuPane.aspx";
   	 	document.location.href = filePath;
   	}   	 			
}

function newFile(path)
{
	var fileName = window.prompt("Enter the name of the new file", "");			
	if(fileName != null)//<%=CurrentExecutionFilePath%>
		document.location.href = "default.aspx?newFile=" + fileName + "&Path=" + path;
}

function newFolder(path)
{
	var folderName = window.prompt("Enter the name of the new folder", "");			
	if(folderName != null)//<%=CurrentExecutionFilePath%>
	
		document.location.href = "default.aspx?newFolder=" + folderName + "&Path=" + path;
}

function rename(filePath, fileName)
{
	var newName = window.prompt("Enter the new name for '" + fileName + "'", fileName);			
	if(newName != null)
		document.location.href = filePath + "&NewName=" + newName;
}

var winOpts = 'resizeable=no,scrollbars=yes,left=125,top=175,width=420,height=105';
function popUp(pPage) 
{
	popUp = window.open(pPage,'popWin',winOpts);
}  
function ReturnValue(txtChoice, newValue)
{
	
	eval('var theform = opener.document.Form1;');
	window.close();
	theform.elements[txtChoice].value = newValue;
}