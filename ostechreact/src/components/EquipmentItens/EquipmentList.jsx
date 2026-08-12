import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const EquipmentList = ({ equipment, onEdit, onDelete }) => {
    return (
        <li className="flex items-center justify-between gap-4 px-4 py-3 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div>
                <p className="text-[#E2E2B6]">
                    <span className="font-semibold text-[#6EACDA]">Name: </span>
                    {equipment.name}
                </p>
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Brand: </span>
                    {equipment.brand}</p>
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Model: </span>
                    {equipment.model}</p>
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Serial Number: </span>
                    {equipment.serialNumber}</p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}