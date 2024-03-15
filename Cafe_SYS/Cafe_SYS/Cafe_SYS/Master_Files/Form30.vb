Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form30
    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form31.TextBox6.Text = "1"
        Form31.Show()
    End Sub

    Private Sub Label18_TextChanged(sender As Object, e As EventArgs) Handles Label18.TextChanged
        filldata()
    End Sub
    Private Sub filldata()
        conn = GetConnect()
        conn.Open()
        SQL = "select  ID,Contact1,Contact2,Address,Email,Note " &
                " from Customers where Code = '" + Label18.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox3.Text = dr.GetValue(0).ToString     'id
            TextBox5.Text = dr.GetValue(1).ToString   'con1
            TextBox6.Text = dr.GetValue(2).ToString    'con2
            TextBox4.Text = dr.GetValue(3).ToString    'add
            TextBox7.Text = dr.GetValue(4).ToString    'emil
            TextBox8.Text = dr.GetValue(5).ToString    'note
            'TextBox9.Text = dr.GetValue(6).ToString   'acc
            'ComboBox1.Text = dr.GetValue(7).ToString   'bnk
        End While

        conn.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Customers set Name ='" + TextBox20.Text + "' where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub
    Private Sub clr()
        Dim Form30 As New Form30
        Me.Hide()
        Form30.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        conn = GetConnect()
        conn.Open()
        SQL = "update Customers set Code ='" + TextBox19.Text + "' where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' "
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conn = GetConnect()
        conn.Open()
        SQL = " update Customers set Contact1 = '" + TextBox5.Text + "' ,Contact2 = '" + TextBox6.Text + "' , " &
                " Address = '" + TextBox4.Text + "' ,Email = '" + TextBox7.Text + "' , " &
                "Date='" + d.ToString(formata) + "', " &
                "Note = '" + TextBox8.Text + "'  " &
            " where  Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' and ID = '" + TextBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully updated !")
        clr()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        conn = GetConnect()
        conn.Open()
        SQL = "delete from Customers  where Name = '" + TextBox1.Text + "' and Code = '" + TextBox2.Text + "' " &
            "and ID = '" + TextBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Successfully Updated !")
        clr()
    End Sub
End Class