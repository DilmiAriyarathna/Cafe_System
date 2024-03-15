Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form19

    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1, cmd2 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr, dr1 As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x, qty, qtytot As Integer
    Dim user As String
    Dim acc_no As String
    Dim emp_name As String
    Dim qty1, qty2, sqty, bqty, eqty As Double
    Dim u_cost As Double
    Dim total As Double
    Dim S_qty, E_qty, B_qty As Double
    ' Dim xqty, yqty, zqty As Double

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form41.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form_S.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Form19_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillcombo()
        ''
        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If
        ''
        BunifuCustomDataGrid1.DefaultCellStyle.Font = New Font("verdena", 7)
        BunifuCustomDataGrid1.ColumnHeadersDefaultCellStyle.Font = New Font("verdena", 7)
        ''
        Displaydata()
        grd()
        ''
        Me.KeyPreview = True
        ''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Count(ID) from Daily_Stock"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString
            End While
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox1.Focus()
        End If
    End Sub
    Public Sub Displaydata()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Products", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 85
        BunifuCustomDataGrid1.Columns(1).Width = 243
    End Sub

    Private Sub BunifuCustomDataGrid1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuCustomDataGrid1.KeyPress
        ''''''''If e.KeyChar = Convert.ToChar(13) Then
        ''''''''    '' BunifuCustomDataGrid1_CellClick(sender, e)
        ''''''''    '' TextBox2.Focus()
        ''''''''    'If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
        ''''''''    '    TextBox3.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
        ''''''''    'End If
        ''''''''    'Handles AnotherButton.Click
        ''''''''    ' zero based ColumnIndex of your button column= 3 (for example)
        ''''''''    ' zero based RowIndex that you want to click on its button column = 2 (for example)
        ''''''''    Dim arg = New DataGridViewCellEventArgs(0, 0)
        ''''''''    BunifuCustomDataGrid1_CellClick(BunifuCustomDataGrid1, arg)
        ''''''''End If
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox3.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(0).Value

            TextBox9.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Label18.Text = "Description" Then
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select Code,Name from Products where Name like '%" + TextBox1.Text + "%'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid1.DataSource = dt
            conn.Close()

        ElseIf Label18.Text = "Code" Then
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select Code,Name from Products where Code like '%" + TextBox1.Text + "%'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid1.DataSource = dt
            conn.Close()
        End If
    End Sub

    Private Sub Form19_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown, TextBox1.KeyDown
        If e.KeyCode = Keys.F1 Then
            If Label18.Text = "Description" Then
                Label18.Text = "Code"
                TextBox1.Focus()
            ElseIf Label18.Text = "Code" Then
                Label18.Text = "Description"
                TextBox1.Focus()
            End If
        End If
    End Sub
    Private Sub fill()
        conn = GetConnect()
        conn.Open()
        SQL = "Select Name , " +
        " CAST(P_Price As numeric(17,2))P_Price , " +
        " CAST(R_Price As numeric(17,2))R_Price, " +
        " CAST(S_Price As numeric(17,2))S_Price from Products where Code ='" + TextBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox2.Text = dr.GetValue(0).ToString
            TextBox8.Text = dr.GetValue(1).ToString
            TextBox14.Text = dr.GetValue(2).ToString
            TextBox12.Text = dr.GetValue(3).ToString
            TextBox15.Text = dr.GetValue(3).ToString
        End While
        conn.Close()
    End Sub
    Private Sub src()
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"
        '''''
        conn = GetConnect()
        conn.Open()
        cmd1 = New SqlCommand("select * from Daily_Stock where Procuct='" + TextBox3.Text + "' and Date = '" + d.ToString(formata) + "'", conn)
        dt = New DataTable()
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())

        If (x > 0) Then
            SQL = "select S_qty from Daily_Stock_Sum where Procuct='" + TextBox3.Text + "' and Date = '" + d.ToString(formata) + "' and flag = '1' "
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                TextBox13.Text = dr.GetValue(0).ToString
            End While
        Else
            TextBox13.Text = "0"
        End If
        conn.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"
        '''''
        conn = GetConnect()
        conn.Open()
        cmd1 = New SqlCommand("select * from Daily_Stock where Procuct='" + TextBox3.Text + "' and Date = '" + d.ToString(formata) + "'", conn)
        dt = New DataTable()
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())

        If (x > 0) Then
            MsgBox("Already Exist Daily End Summary !")
            clr()
            Exit Sub
        Else
            SQL = "insert into Daily_Stock_Summary(Date,Login,Procuct,P_Price,R_Price,S_Price, " &
                "S_qty,E_qty,B_qty,flag,aflag,cashier,acc_no,f_login) values ('" + d.ToString(formata) + "' , " &
                " '" + user + "' , '" + TextBox3.Text + "' , '" + TextBox8.Text + "' ,'" + TextBox14.Text + "' ," &
                "'" + TextBox12.Text + "' , '" + TextBox13.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "' , " &
                "'5','5','" + emp_name + "','" + acc_no + "','" + ComboBox1.Text + "')"
            '''''SQL = "update Daily_Stock set E_qty='" + TextBox4.Text + "',B_qty='" + TextBox5.Text + "', " &
            '''''"elogin='" + ComboBox1.Text + "',ecashier='" + emp_name + "',eacc='" + acc_no + "' ," &
            '''''" flag='2',aflag='2'" &
            '''''        "where Procuct = '" + TextBox3.Text + "' and  Date = '" + d.ToString(formata) + "'"
            cmd2 = New SqlCommand(SQL, conn)
            cmd2.ExecuteNonQuery()
            MsgBox("Update Successfully !")
        End If
        conn.Close()

    End Sub

    Private Sub ADD()

        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"
        ''''''
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()
        ''''''
        conn = GetConnect()
        conn.Open()
        'cmd1 = New SqlCommand("select * from Daily_Stock where Procuct='" + TextBox3.Text + "' and Date = '" + d.ToString(formata) + "'", conn)
        'dt = New DataTable()
        'da = New SqlDataAdapter(cmd1)
        'da.Fill(dt)
        'x = Convert.ToInt32(dt.Rows.Count.ToString())

        'If (x > 0) Then
        '    SQL = "update Daily_Stock set P_Price='" + TextBox8.Text + "' , " &
        '               "R_Price = '" + TextBox14.Text + "' ,S_Price = '" + TextBox12.Text + "' ," &
        '               "cashier='" + emp_name + "',acc_no='" + acc_no + "',Login='" + ComboBox1.Text + "' " &
        '                    "where Procuct = '" + TextBox3.Text + "' and  Date = '" + d.ToString(formata) + "'"
        '    cmd2 = New SqlCommand(SQL, conn)
        '    cmd2.ExecuteNonQuery()
        'Else
        SQL = "insert into Daily_Stock (ID,Date,Login,Procuct,P_Price,R_Price,S_Price, " &
                "P_qty,D_qty,S_qty,Total,E_qty,B_qty,flag,aflag,cashier,acc_no) values ( " &
                " '" + TextBox6.Text + "' , '" + d.ToString(formata) + "' , '" + ComboBox1.Text + "' , " &
                " '" + TextBox3.Text + "' , '" + TextBox8.Text + "' , '" + TextBox14.Text + "' ," &
                " '" + TextBox12.Text + "' , '" + TextBox9.Text + "' , '" + TextBox10.Text + "' , " &
                " '" + TextBox11.Text + "' , '" + TextBox7.Text + "' , '" + TextBox4.Text + "' , " &
                " '" + TextBox5.Text + "' , '1', '1' , '" + emp_name + "' , '" + acc_no + "')"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        ' End If

        conn.Close()
    End Sub
    Private Sub pr_updt()
        conn = GetConnect()
        conn.Open()
        SQL = "update Products set P_Price='" + TextBox8.Text + "' , " &
            "R_Price = '" + TextBox14.Text + "' ,S_Price = '" + TextBox12.Text + "' where Code = '" + TextBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''''''
        conn = GetConnect()
        conn.Open()
        SQL = "update Selling_Price set O_sp = '" + TextBox15.Text + "' , N_sp='" + TextBox12.Text + "'  where Code = '" + TextBox3.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged


    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            S_qty = Val(TextBox13.Text)
            E_qty = Val(TextBox4.Text)

            B_qty = S_qty - E_qty

            TextBox5.Text = Val(B_qty)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        ADD()
        pr_updt()
        ''

        conn = GetConnect()

        SQL = "select * from Stock_Qty2 where Code='" + TextBox3.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            conn.Open()
            SQL = "select * from Stock_Qty2 where Code='" + TextBox3.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                qty = dr.GetValue(1).ToString
            End While
            conn.Close()
            ''
            qtytot = qty + TextBox11.Text
            ''
            conn.Open()
            SQL = "Update Stock_Qty2 set QTY= @QTY,Final_Cost=@Final_Cost where Code='" + TextBox3.Text + "'"
            cmdq = New SqlCommand(SQL, conn)
            With cmdq.Parameters
                .AddWithValue("@Code", TextBox3.Text)
                .AddWithValue("@QTY", qtytot)
                .AddWithValue("@Final_Cost", TextBox12.Text)
            End With
            cmdq.ExecuteNonQuery()
            conn.Close()
        Else
            conn.Open()
            SQL = "insert into Stock_Qty2 (Code,QTY,Final_Cost) values(@Code,@QTY,@Final_Cost)"
            cmdq = New SqlCommand(SQL, conn)
            With cmdq.Parameters
                .AddWithValue("@Code", TextBox3.Text)
                .AddWithValue("@QTY", TextBox11.Text)
                .AddWithValue("@Final_Cost", TextBox12.Text)
            End With
            cmdq.ExecuteNonQuery()
            conn.Close()
        End If

        MsgBox("Update Successfully !")
        clr()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        clr()
    End Sub

    Private Sub clr()
        Dim form19 As New Form19
        Me.Hide()
        form19.Show()
    End Sub
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        fill()
        src()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox10.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            '' TextBox1.Focus()

            qty1 = Val(TextBox9.Text)
                qty2 = Val(TextBox10.Text)

                sqty = qty1 - qty2

            TextBox11.Text = Val(sqty)
            TextBox13.Text = Val(TextBox13.Text) + Val(sqty)

            u_cost = Val(TextBox8.Text)

                total = u_cost * sqty
                TextBox7.Text = total.ToString("#.00")

        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "Select Acc_No from Usert where UName = '" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            acc_no = dr.GetValue(0).ToString
        End While
        conn.Close()
        '''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Employee where Acc_No = '" + acc_no + "'"
        cmd1 = New SqlCommand(SQL, conn)
        dr1 = cmd1.ExecuteReader
        While (dr1.Read())
            emp_name = dr1.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub fillcombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select UName from Usert"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
End Class