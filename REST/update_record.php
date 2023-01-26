<?php 

include_once('connects.php');
$name = $_GET['name'];
$school =  $_GET['school'];
$country = $_GET['country'];
$gender =  $_GET['gender'];

$result = mysqli_query($con,"UPDATE `studentdata` SET `name`='".$name."',`school`='".$school."',`country`='".$country."',`gender`='".$gender."' Where `name`='".$name."'");
echo "Data Updated";
?>

