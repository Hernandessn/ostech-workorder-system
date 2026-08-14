import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Category } from "./pages/Category";
import { App } from "./App";
import { Customer } from "./pages/Customer";
import { Equipment } from "./pages/Equipment";
import { WorkOrder } from "./pages/WorkOrder";
import { Technician } from "./pages/Technician";
import { NotFound } from "./pages/NotFound";

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" exact Component={App} />
                <Route path="/categories" exact Component={Category} />
                <Route path="/customers" Component={Customer} />
                <Route path="/equipments" Component={Equipment} />
                <Route path="/workorders" Component={WorkOrder} />
                <Route path="/technicians" Component={Technician} />
                <Route path="*" Component={NotFound} />
            </Routes>
        </BrowserRouter>
    );
}