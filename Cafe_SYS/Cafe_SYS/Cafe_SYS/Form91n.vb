Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form91n
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Private Sub Form91n_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = "Search here....."
        TextBox1.ForeColor = Color.Gray
        TextBox1.Font = New Font(Font, FontStyle.Italic)
        ''''''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT count(ID) FROM User_Privileges"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception
        End Try
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''''''''''
        Display()
        grd()
        Fillcombo()

        ComboBox1.Focus()
        ComboBox1.DroppedDown = True
    End Sub

    Private Sub Fillcombo()

        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Au_Level"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select MainCat from Form_Mcat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select SubCat from Form_Scat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox3.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        TextBox1.Text = ""
        TextBox1.ForeColor = Color.Black
        TextBox1.Font = New Font(Font, FontStyle.Regular)
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 100
        BunifuCustomDataGrid1.Columns(1).Width = 145
        BunifuCustomDataGrid1.Columns(2).Width = 300

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select ID,Name as Form, Caption as Caption from Form_List where Caption like '%" + TextBox1.Text + "%'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox2.Focus()
            ComboBox2.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            conn = GetConnect()
            conn.Open()
            SQL = "select emp_code from Usert where UName ='" + ComboBox2.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                Label11.Text = dr.GetValue(0).ToString
                'Label18.Text = dr.GetValue(0).ToString
            End While

            ComboBox4.Focus()
            ComboBox4.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If (ComboBox4.Text = "Reports") Then
                ComboBox3.Enabled = True
                ComboBox3.Focus()
                ComboBox3.DroppedDown = True
            Else
                '' TextBox1.Focus() 
                CheckBox1.Focus()

                If CheckBox1.Focus = True Then
                    CheckBox1.BackColor = Color.Tan
                    CheckBox2.BackColor = Color.LightGray
                Else
                    CheckBox1.BackColor = Color.LightGray
                End If

                ''  ComboBox2.DroppedDown = True

                If CheckBox1.Checked = True Then
                    CheckBox2.Enabled = False
                    Button1.Focus()
                    '   CheckBox1.BackColor = Color.Tan
                    ' Else
                    '  CheckBox1.BackColor = Color.LightGray
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (CheckBox1.Checked = False And CheckBox2.Checked = False) Then
            MsgBox("Invalid Sellection !")
            CheckBox1.Focus()
            Exit Sub
        End If

        If (CheckBox1.Checked = True) Then
            conn = GetConnect()
            conn.Open()
            If (ComboBox3.Enabled = False) Then
                da = New SqlDataAdapter("select ID,Name as Form, Caption as Caption from Form_List " &
                                    " where " &
                                    " flag = '1' and Category='" + ComboBox4.Text + "'", conn)
                dt = New DataTable()
                da.Fill(dt)
                BunifuCustomDataGrid1.DataSource = dt
            Else
                da = New SqlDataAdapter("select ID,Name as Form, Caption as Caption from Form_List " &
                                   " where " &
                                   " flag = '1' and Category='" + ComboBox4.Text + "' and S_cat='" + ComboBox3.Text + "'", conn)
                dt = New DataTable()
                da.Fill(dt)
                BunifuCustomDataGrid1.DataSource = dt
            End If
            conn.Close()

        ElseIf (CheckBox2.Checked = True) Then
            conn = GetConnect()
            conn.Open()
            If (ComboBox3.Enabled = False) Then
                da = New SqlDataAdapter("select ID,Name as Form, Caption as Caption from Form_List " &
                                    " where  " &
                                    " flag = '2' and Category='" + ComboBox4.Text + "'", conn)
                dt = New DataTable()
                da.Fill(dt)
                BunifuCustomDataGrid1.DataSource = dt
            Else
                da = New SqlDataAdapter("select ID,Name as Form, Caption as Caption from Form_List " &
                                   " where and " &
                                   " flag = '2' and Category='" + ComboBox4.Text + "' and S_cat='" + ComboBox3.Text + "'", conn)
                dt = New DataTable()
                da.Fill(dt)
                BunifuCustomDataGrid1.DataSource = dt
            End If
            conn.Close()
        End If

    End Sub

    Private Sub BunifuSeparator11_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If (CheckBox1.Checked = True) Then
            CheckBox2.Enabled = False
        Else
            CheckBox2.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If (CheckBox2.Checked = True) Then
            CheckBox1.Enabled = False
        Else
            CheckBox1.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim form91n As New Form91n
        Me.Hide()
        form91n.Show()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox2.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(0).Value
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Filldata()
    End Sub
    Private Sub Filldata()
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Form_List where ID ='" + TextBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            'ComboBox1.Text = dr.GetValue(1).ToString
            'ComboBox2.Text = dr.GetValue(2).ToString
            TextBox4.Text = dr.GetValue(0).ToString
            TextBox5.Text = dr.GetValue(1).ToString
            TextBox7.Text = dr.GetValue(2).ToString
            TextBox8.Text = dr.GetValue(7).ToString
            TextBox3.Text = dr.GetValue(9).ToString
            Label14.Text = dr.GetValue(3).ToString
            Label15.Text = dr.GetValue(4).ToString
            Label16.Text = dr.GetValue(5).ToString
            Label17.Text = dr.GetValue(6).ToString
        End While

        conn.Close()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If (TextBox3.Text = 1) Then
            Button2.Visible = True
            Button4.Visible = True
        ElseIf (TextBox3.Text = 2) Then
            Button6.Visible = True
            Button7.Visible = True
        ElseIf (TextBox3.Text = "") Then
            Button2.Visible = True
            Button4.Visible = True
            Button6.Visible = True
            Button7.Visible = True
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub
    Private Sub addup()
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()

        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"

        conn = GetConnect()
        conn.Open()
        SQL = "insert into User_Privileges(ID,Usernm,acc_no,Caption,Name,o_Id,F_id,Mcat,Scat,Project, " &
        "Date,Time,U_prfl,flag1,usrt,flag2) values ( " &
        " '" + TextBox6.Text + "' , " &
        " '" + Label12.Text + "' , " &
        " '" + Label13.Text + "' , " &
        " '" + TextBox5.Text + "' , " &
        " '" + TextBox7.Text + "' , " &
        " '" + Label17.Text + "' , " &
        " '" + Label16.Text + "' , " &
        " '" + Label14.Text + "' , " &
        " '" + Label15.Text + "' , " &
        " '" + ComboBox1.Text + "' , " &
        " '" + d.ToString(formata) + "' , " &
        " '" + t.ToString(formatb) + "' , " &
        " '" + Label18.Text + "' , " &
        " '1' , '" + ComboBox2.Text + "' , '1' )"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ' MsgBox("Updated Successfully !")
        conn.Close()

        conn = GetConnect()
        conn.Open()
        SQL = "update Form_List set flag = '2' where Caption='" + TextBox5.Text + "' and Name='" + TextBox7.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        MsgBox("Updated Successfully !")
        conn.Close()
        Clr()
    End Sub
    Private Sub remup()
        conn = GetConnect()
        conn.Open()
        SQL = "delete from User_Privileges where Caption='" + TextBox5.Text + "' and Name='" + TextBox7.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        'MsgBox("Updated Successfully !")
        conn.Close()

        conn = GetConnect()
        conn.Open()
        SQL = "update Form_List set flag = '1' where Caption='" + TextBox5.Text + "' and Name='" + TextBox7.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        cmd1.ExecuteNonQuery()
        conn.Close()
        MsgBox("Updated Successfully !")
        conn.Close()

        Clr()
    End Sub
    Private Sub Clr()
        Dim form91n As New Form91n
        Me.Hide()
        form91n.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        addup()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        remup()
    End Sub

    Private Sub Label11_TextChanged(sender As Object, e As EventArgs) Handles Label11.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Name,Acc_No from Employee where empid ='" + Label11.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label12.Text = dr.GetValue(0).ToString
            Label13.Text = dr.GetValue(1).ToString

        End While
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Code from Au_Level where Name ='" + ComboBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label18.Text = dr.GetValue(0).ToString
        End While
    End Sub

    Private Sub BunifuCustomDataGrid1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellContentClick

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox1.Focus()

            If CheckBox1.Focus = True Then
                CheckBox1.BackColor = Color.Tan
                CheckBox2.BackColor = Color.LightGray
            Else
                CheckBox1.BackColor = Color.LightGray
            End If

            ''  ComboBox2.DroppedDown = True

            If CheckBox1.Checked = True Then
                CheckBox2.Enabled = False
                Button1.Focus()
                '   CheckBox1.BackColor = Color.Tan
                ' Else
                '  CheckBox1.BackColor = Color.LightGray
            End If
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox2.Enabled = True Then
                CheckBox2.Focus()
                If CheckBox2.Focus = True Then
                    CheckBox2.BackColor = Color.Tan
                    CheckBox1.BackColor = Color.LightGray
                Else
                    CheckBox1.BackColor = Color.LightGray
                End If
                If CheckBox1.Checked = True Then
                    CheckBox1.Enabled = False
                    Button1.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select ID, Name as Form, Caption as Caption from Form_List", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
End Class