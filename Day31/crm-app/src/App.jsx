import { useEffect, useState } from "react";

function App() {
  const [customers, setCustomers] = useState([]);
  const [search, setSearch] = useState("");
  const [showForm, setShowForm] = useState(false);

  const [newCustomer, setNewCustomer] = useState({
    name: "",
    email: "",
    company: "",
    phone: "",
  });

  // 🔹 Fetch customers
  const fetchCustomers = () => {
    fetch("http://localhost:5263/api/customers")
      .then((res) => res.json())
      .then((data) => setCustomers(data))
      .catch((err) => console.error(err));
  };

  useEffect(() => {
    fetchCustomers();
  }, []);

  // 🔍 Search filter
  const filteredCustomers = customers.filter(
    (c) =>
      (c.name || "").toLowerCase().includes(search.toLowerCase()) ||
      (c.company || "").toLowerCase().includes(search.toLowerCase())
  );

  // 🔥 Toggle status (UI only)
  const toggleStatus = (id) => {
    const updated = customers.map((c) =>
      c.id === id ? { ...c, isActive: !c.isActive } : c
    );
    setCustomers(updated);
  };

  // 🔹 Handle input
  const handleChange = (e) => {
    setNewCustomer({ ...newCustomer, [e.target.name]: e.target.value });
  };

  // 🔥 Add customer (DB insert)
  const addCustomer = () => {
    fetch("http://localhost:5263/api/customers", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newCustomer),
    })
      .then(() => {
        fetchCustomers(); // refresh table
        setShowForm(false);
        setNewCustomer({ name: "", email: "", company: "", phone: "" });
      })
      .catch((err) => console.error(err));
  };

  return (
    <div style={styles.container}>
      
      {/* 🔷 HEADER */}
      <div style={styles.header}>
        <h2>Customer Management</h2>
        <button style={styles.addBtn} onClick={() => setShowForm(!showForm)}>
          + Add Customer
        </button>
      </div>

      {/* 🔍 SEARCH */}
      <input
        type="text"
        placeholder="Search by name or company..."
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        style={styles.search}
      />

      <p>{filteredCustomers.length} customers</p>

      {/* 🧾 ADD CUSTOMER FORM */}
      {showForm && (
        <div style={styles.form}>
          <input name="name" placeholder="Name" onChange={handleChange} />
          <input name="email" placeholder="Email" onChange={handleChange} />
          <input name="company" placeholder="Company" onChange={handleChange} />
          <input name="phone" placeholder="Phone" onChange={handleChange} />

          <button style={styles.saveBtn} onClick={addCustomer}>
            Save
          </button>
        </div>
      )}

      {/* 📊 TABLE */}
      <div style={styles.tableWrapper}>
        <table style={styles.table}>
          <thead>
            <tr>
              <th style={styles.th}>ID</th>
              <th style={styles.th}>Name</th>
              <th style={styles.th}>Email</th>
              <th style={styles.th}>Company</th>
              <th style={styles.th}>Phone</th>
              <th style={styles.th}>Status</th>
            </tr>
          </thead>

          <tbody>
            {filteredCustomers.length > 0 ? (
              filteredCustomers.map((c) => (
                <tr key={c.id}>
                  <td style={styles.td}>{c.id}</td>
                  <td style={styles.td}>{c.name}</td>
                  <td style={styles.td}>{c.email}</td>
                  <td style={styles.td}>{c.company}</td>
                  <td style={styles.td}>{c.phone}</td>

                  <td style={styles.td}>
                    <button
                      onClick={() => toggleStatus(c.id)}
                      style={{
                        backgroundColor: c.isActive ? "#28a745" : "#dc3545",
                        color: "white",
                        border: "none",
                        padding: "5px 12px",
                        borderRadius: "20px",
                        cursor: "pointer",
                      }}
                    >
                      {c.isActive ? "Active" : "Inactive"}
                    </button>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="6" style={styles.noData}>
                  No customers found
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

// 🎨 STYLES
const styles = {
  container: {
    padding: "30px",
    fontFamily: "Arial",
    backgroundColor: "#f4f6f8",
    minHeight: "100vh",
  },
  header: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: "15px",
  },
  addBtn: {
    backgroundColor: "#007bff",
    color: "white",
    border: "none",
    padding: "8px 15px",
    borderRadius: "5px",
    cursor: "pointer",
  },
  search: {
    width: "100%",
    padding: "10px",
    marginBottom: "10px",
    borderRadius: "5px",
    border: "1px solid #ccc",
  },
  form: {
    marginBottom: "15px",
    display: "flex",
    gap: "10px",
    flexWrap: "wrap",
  },
  saveBtn: {
    backgroundColor: "green",
    color: "white",
    border: "none",
    padding: "8px 15px",
    borderRadius: "5px",
    cursor: "pointer",
  },
  tableWrapper: {
    overflowX: "auto",
  },
  table: {
    width: "100%",
    borderCollapse: "collapse",
    backgroundColor: "white",
  },
  th: {
    backgroundColor: "#007bff",
    color: "white",
    padding: "10px",
  },
  td: {
    padding: "10px",
    borderBottom: "1px solid #ddd",
  },
  noData: {
    textAlign: "center",
    padding: "15px",
  },
};

export default App;