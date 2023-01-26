<?php 

include_once('connects.php');
$name = $_GET['name'];
$school =  $_GET['school'];
$country = $_GET['country'];
$gender =  $_GET['gender'];

$result = mysqli_query($con,"DELETE FROM `studentdata` Where `name`='".$name."'");
echo "Data Remove";
?>

