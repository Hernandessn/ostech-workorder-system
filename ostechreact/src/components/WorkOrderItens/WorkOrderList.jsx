import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const WorkOrderList = ({ workOrder, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="font-medium">{workOrder.name}</p>
                <p className="font-medium">{workOrder.description}</p>
                <p className="font-medium">{workOrder.amount}</p>
                <p className="font-medium">{workOrder.deadline}</p>
                <p className="font-medium">{workOrder.openingDate}</p>
                <p className="font-medium">{workOrder.customerId}</p>
                <p className="font-medium">{workOrder.categoryId}</p>
                <p className="font-medium">{workOrder.equipmentId}</p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}