import { useEffect, useState } from "react";
import { addUser, getAdminUsers, updateUser, deleteUser } from "../../Api/Api";
import MaterialTable from "@material-table/core";
export default function AdminPage() {
  const columns = [
    {
      field: "id",
      title: "Id",
      editable: "never",
    },
    {
      field: "role",
      title: "Role",
      lookup: { admin: "Admin", user: "User" },
    },
    {
      field: "username",
      title: "username",
    },
    {
      field: "password",
      title: "Password",
    },
  ];
  const [data, setData] = useState([]);
  useEffect(() => {
    getAdminUsers()
      .then((response) => {
        const { data } = response;
        setData(data);
      })
      .catch(() => {
        window.localStorage.clear();
        window.location.reload();
      });
  }, []);
  return (
    <div style={{ padding: 10 }}>
      <MaterialTable
        columns={columns}
        data={data}
        title="Users"
        options={{
          actionsColumnIndex: -1,
        }}
        editable={{
          onRowAdd: (newData) =>
            new Promise((resolve, reject) => {
              setTimeout(() => {
                addUser(newData)
                  .then((response) => {
                    const responseData = response.data;
                    const newData = [...data];
                    newData.push(responseData);
                    setData(newData);
                    resolve();
                  })
                  .catch(() => {
                    reject();
                  });
              }, 1000);
            }),
          onRowUpdate: (newData, oldData) =>
            new Promise((resolve, reject) => {
              setTimeout(() => {
                updateUser(newData)
                  .then(() => {
                    const tempData = data.map((u) =>
                      u.id === oldData.id
                        ? {
                            ...u,
                            username: newData.username,
                            password: newData.password,
                            role: newData.role,
                          }
                        : u
                    );
                    setData(tempData);
                    resolve();
                  })
                  .catch(() => {
                    reject();
                  });
              }, 1000);
            }),
          onRowDelete: (oldData) =>
            new Promise((resolve, reject) => {
              setTimeout(() => {
                const userId = oldData.id;
                deleteUser(userId)
                  .then(() => {
                    const tempData = data.filter((u) => u.id !== userId);
                    setData(tempData);
                    resolve();
                  })
                  .catch(() => {
                    reject();
                  });
              }, 1000);
            }),
        }}
      />
    </div>
  );
}
