
<?php

include_once('connects.php');

	
	$username = $_GET['uname'];
	$password = $_GET['pword'];

	
	$result = mysqli_query($con,"SELECT * FROM usertable WHERE uname = '$username' AND pword = '$password'");
		
	if(!$row = mysqli_fetch_assoc($result)) 
        {
        echo "Failed!";
	} 
	else 
	{
	echo "OK!";	
	}

	mysqli_close($con);
?>