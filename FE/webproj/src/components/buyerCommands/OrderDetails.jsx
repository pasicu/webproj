import React, {useEffect, useState} from 'react';
import { useLocation, Link } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { GetOrderDetails, IsOrderDelivered } from '../../services/OrderServices';


export const OrderDetails = () => {
    const logedInUser = JSON.parse(localStorage.getItem("logedInUser"));
    const [userType, setUserType] = useState(logedInUser.userType);
    const location = useLocation();
    const [orderDetails, setOrderDetails] = useState({products: []});
    const token = localStorage.getItem("token"); 
    const { pOrderId } = location.state;

    const handleAlert = (message, type) => {
        if(type === "success")
            toast.success(message);
        else
            toast.error(message);
    }

    useEffect( () => {
        const getOrderDetails = async () => {
            try{ 
                const response = await GetOrderDetails(handleAlert,token, pOrderId, userType);
                setOrderDetails(response);
            }
            catch(ex) { 
                console.log(ex);
            }
        };
        getOrderDetails();
    },[]);


    return(
        <>
        <Link className='link-button' to='/dashboard'>
            <button className="back-to-dashboard-button">Dashboard</button>
        </Link> 
        <ToastContainer/>
        <h2 style={{color:"white"}}>Order details</h2>
        <table className='verify-sellers-table'>
            <tr className="verify-sellers-table-header-row">
                <th>Ordered at</th>
                <th>Delivery time</th>
                <th>Total price</th>
                <th>Delivery address</th>
                <th>Your comment</th>
                <th>Status</th>
            </tr>
            <tr>
                <td>{orderDetails.orderedAt}</td>
                <td>
                    {orderDetails.isCanceled === true ?(
                          <p style={{color:"white"}}>Order is canceled</p>
                    ):
                    (orderDetails.deliveringTime)
                    } 
                </td>
                <td>{orderDetails.totalPrice}</td>
                <td>{orderDetails.address}</td>
                <td>{orderDetails.comment}</td>
                <td>
                    {orderDetails.isCanceled === true ?(
                        <p style={{color:"white"}}>Order is canceled</p>
                    ) : IsOrderDelivered(orderDetails.deliveringTime) ? (
                        <p style={{color:"white"}}>Order is delivered</p>
                    ) : (
                        <p style={{color:"white"}}>
                            It will be delivered soon.
                        </p>
                    )}                    
                </td>
            </tr>
        </table>
        <br/><br/>
        <h3 style={{color:"white", textAlign:"left"}}>Ordered products:</h3>
        <table className='verify-sellers-table'>
            <tr className='verify-sellers-table-header-row'>
                <th>Image</th>
                <th>Name</th>
                <th>Description</th>
                <th>Price per unit</th>
                <th>Units ordered</th>
            </tr>
            {orderDetails.products.map(product => (
            <tr key={product.id}>
                <td>
                    <img className='profile-picture' src={product.image} alt="No picture"/>
                </td>
                <td>{product.name}</td>
                <td>{product.description}</td>
                <td>{product.price}</td>
                <td>{product.quantity}</td>
            </tr>
            ))}
        </table>
        </>
    )
}