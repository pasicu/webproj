import './App.css';
import { Route, Routes } from 'react-router-dom';
import { Login } from './components/Login';
import { Register } from './components/Register';
import { Dashboard } from './components/Dashboard';
import { MyProfile } from './components/MyProfile';
import { PrivateRoute } from "./components/PrivateRoutes";
import { MyProducts } from './components/sellerCommands/MyProducts';
import { AddNewProduct } from './components/sellerCommands/AddNewProduct';

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Login/>}/>
        <Route path="/register" element={<Register/>}/>
        <Route path="/login" element={<Login/>}/>
        <Route path="/my-profile" 
                element={<PrivateRoute 
                allowedRoles={["Admin", "Seller", "Buyer"]}  
                component={MyProfile} />} />
        <Route path="/dashboard"
                element= {<PrivateRoute
                          allowedRoles={["Admin", "Seller", "Buyer"]}
                          component={Dashboard} />} />
        <Route path="/my-products"
                element={<PrivateRoute
                          allowedRoles={["Seller"]}
                          component={MyProducts} />} /> 
        <Route path="/add-new-product"
                element={<PrivateRoute
                          allowedRoles={["Seller"]}
                          component={AddNewProduct}/> }/>                           
      </Routes>
    </>
  );
}

export default App;
