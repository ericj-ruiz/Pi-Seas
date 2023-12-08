<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title> Pi-Seas </title>
    <meta charset="utf-8" />
    <?php
        require_once("config.php");
    ?>

    <link href="https://nrs-projects.humboldt.edu/~st10/styles/normalize.css" type="text/css" rel="stylesheet" />
    <link href="highScore.css" type="text/css" rel="stylesheet" />
</head>

<body>
    <h1> Pi-Seas </h1>
    <h2> By MEDS </h2>
    <img id="img" src="https://i.imgur.com/eyJ5wvf.png" alt="Pi-seas" title="PI_SEAS!" />

    <?php
    
        $username = dbUsername;
        $password = dbPassword;

    //connecting        
        $db_conn_str = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)
                                       (HOST = cedar.humboldt.edu)
                                       (PORT = 1521))
                                   (CONNECT_DATA = (SID = STUDENT)))";

        $conn = oci_connect($username, $password, $db_conn_str);

        
        if (!$conn) {
            ?>
            <p> Could not log into Pi-Seas, sorry </p>
            <?php
            exit;
        }

        // query for table
        $highScoreQuery = "select initials, score, time, numberOfKills, dateOfWin
                           from (select initials, score, time, numberOfKills, dateOfWin
                                 from GameWinners
                                 order by score desc, time, numberOfKills desc)
                           where rownum <=10";

        $highScoreStmt = oci_parse($conn, $highScoreQuery);
        oci_execute($highScoreStmt, OCI_DEFAULT);
    ?>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>
	<br/>

    <div class="table-container">
        <table class="centered-table">
            <caption> Top 10 High Scores </caption>
            <tr>
                <th scope="col"></th>
                <th scope="col"> Initials </th>
                <th scope="col"> Score </th>
                <th scope="col"> Time </th>
                <th scope="col"> Kills </th> 
                <th scope="col"> Date </th>
            </tr>

            <?php
            $place = 1;
            while (oci_fetch($highScoreStmt)) {
                //  results
                $currInitials = oci_result($highScoreStmt, "INITIALS");
                $currScore = oci_result($highScoreStmt, "SCORE");
                $currTime = oci_result($highScoreStmt, "TIME");
                $currKills = oci_result($highScoreStmt, "NUMBEROFKILLS");
                $currDate = oci_result($highScoreStmt, "DATEOFWIN");
                $formattedTime = gmdate('i:s', $currTime);
            ?>
                <tr>
                    <td><?= $place++ ?></td> 
                    <td><?= strtoupper($currInitials) ?></td>
                    <td><?= $currScore ?></td>
                    <td><?= $formattedTime ?></td>
                    <td><?= $currKills ?></td>
                    <td><?= $currDate ?></td>
                </tr>
            <?php
            }
            ?>
        </table>
    </div>
	<br/>
	<br/>
    <hr/>
    <?php
        oci_free_statement($highScoreStmt);
        oci_close($conn);
    ?>
    <footer>
    </footer>
</body>
</html>
