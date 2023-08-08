import './App.css';
import { Route, Routes } from 'react-router-dom';
import { Login } from './components/Login';
import { Register } from './components/Register';
import { Dashboard } from './components/Dashboard';
import { MyProfile } from './components/MyProfile';
import { PrivateRoute } from "./components/PrivateRoutes";
import { MyProducts } from './components/sellerCommands/MyProducts';
import { AddNewProduct } from './components/sellerCommands/AddNewProduct';
import { NewOrder } from './components/buyerCommands/NewOrder';
import { UpdateProductForm } from './components/sellerCommands/UpdateProduct';
import { OldOrders } from './components/buyerCommands/OldOrders';
import { OrderDetails } from './components/buyerCommands/OrderDetails';
import { NewOrders } from './components/sellerCommands/NewOrders';


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
        <Route path="/update-product"
                element={<PrivateRoute
                          allowedRoles={["Seller"]}
                          component={UpdateProductForm}/> }/>         
        <Route path="/new-order"
                element={<PrivateRoute
                          allowedRoles={["Buyer"]}
                          component={NewOrder}/> }/> 
        <Route path="/old-orders"
                element={<PrivateRoute
                          allowedRoles={["Buyer"]}
                          component={OldOrders}/> }/>
        <Route path="/order-details"
                element={<PrivateRoute
                          allowedRoles={["Buyer", "Admin", "Seller"]}
                          component={OrderDetails}/> }/> 
        <Route path="/new-orders"
                element={<PrivateRoute
                          allowedRoles={["Seller"]}
                          component={NewOrders}/> }/>                                      
      </Routes>
    </>
  );
}

export default App;
