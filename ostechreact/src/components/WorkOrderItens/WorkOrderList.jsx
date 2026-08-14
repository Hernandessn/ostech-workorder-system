import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const WorkOrderList = ({ workOrder, onEdit, onDelete }) => {
    return (
        <li className="flex items-start sm:items-center justify-between gap-4 px-4 py-4 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div className="flex flex-col gap-1">
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Title: </span>
                    {workOrder.title}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Description: </span>
                    {workOrder.description}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Amount: </span>
                    {workOrder.amount}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Deadline: </span>
                    {workOrder.deadline}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Opening Date: </span>
                    {workOrder.openingDate}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Customer ID: </span>
                    {workOrder.customerId}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Category ID: </span>
                    {workOrder.categoryId}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Equipment ID: </span>
                    {workOrder.equipmentId}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Technician ID: </span>
                    {workOrder.technicianId}
                </p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}