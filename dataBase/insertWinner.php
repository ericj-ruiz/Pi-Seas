<?php
require_once("config.php");

$dbUsername =dbUsername;
$dbPassword =dbPassword;

$db_conn_str = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)
                                           (HOST = cedar.humboldt.edu)
                                           (PORT = 1521))
                                       (CONNECT_DATA = (SID = STUDENT)))";
$conn = oci_connect($dbUsername, $dbPassword, $db_conn_str);

// check for a valid connection
if (!$conn) {
    // handle the error properly
    die("Database connection error");
}


if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    // extract data from POST
    $initials = $_POST['initials'];
    $score = $_POST['score'];
    $time = $_POST['time'];
    $kills = $_POST['kills'];
    $date = $_POST['date']; 

    $stmt = oci_parse($conn, "INSERT INTO GameWinners (initials, score, time, numberOfKills, dateOfWin) 
			      VALUES (:initials, :score, :timeVar, :kills, TO_DATE(:winDate, 'YYYY-MM-DD'))");

    // bind the data
    oci_bind_by_name($stmt, ':initials', $initials);
    oci_bind_by_name($stmt, ':score', $score);
    oci_bind_by_name($stmt, ':timeVar', $time);
    oci_bind_by_name($stmt, ':kills', $kills);
    oci_bind_by_name($stmt, ':winDate', $date);

    if (oci_execute($stmt)) {
        oci_commit($conn);
        echo "Success";
    } else {
        echo "Error";
    }

    // close the statement and connection
    oci_free_statement($stmt);
    oci_close($conn);
}
?>
