<?php 

include_once('connects.php');
$name = $_GET['name'];
$school =  $_GET['school'];
$country = $_GET['country'];
$gender =  $_GET['gender'];

$result = mysqli_query($con,"INSERT INTO studentdata (name,school,country,gender) VALUES('$name','$school','$country','$gender')");
echo "Data Inserted";

?>