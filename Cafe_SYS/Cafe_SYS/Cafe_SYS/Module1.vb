

'Module Module1
'    Public dataset As New ADODB.Recordset
'    Public SQL As String

'    Public Function mddata(SQL As String) As ADODB.Recordset
'        Dim conn As New ADODB.Connection
'        Dim cmdcommand As ADODB.Command
'        Dim rr_recordset As New ADODB.Recordset
'        Dim rs_recordset As New ADODB.Recordset
'        Dim pstr As String
'        conn.Provider = "sqloledb"
'        Dim sconn As String
'        sconn = "Server="
'        sconn = "Server=DESKTOP-KTDP3JR\SQLEXPRESS"
'        pstr = sconn + ";Initial Catalog=Cafe_SYS;Integrated Security=True"
'        conn.Open(pstr)

'        With cmdcommand
'            .ActiveConnection = conn
'            .CommandText = SQL
'            .CommandTimeout = 200
'            .CommandType = ADODB.CommandTypeEnum.adCmdText

'        End With

'        With rs_recordset
'            .CursorType = ADODB.CursorTypeEnum.adOpenStatic
'            .CursorLocation = ADODB.CursorLocationEnum.adUseClient
'            .LockType = ADODB.LockTypeEnum.adLockOptimistic
'            .Open(cmdcommand)
'        End With
'    End Function
'End Module
