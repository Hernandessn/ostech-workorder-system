import { ActionsButtons } from '../../components/Buttons/ActionsButtons';

export const EquipmentList = ({ equipment, onEdit, onDelete }) => {
    return (
        <li className="flex items-start sm:items-center justify-between gap-4 px-4 py-4 rounded-md bg-[#03346E] text-[#E2E2B6]">
            <div className="flex flex-col gap-1">
                <p className="font-medium">
                    <span className="font-semibold text-[#6EACDA]">Name: </span>
                    {equipment.name}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Brand: </span>
                    {equipment.brand}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Model: </span>
                    {equipment.model}
                </p>
                <p className="text-sm">
                    <span className="font-semibold text-[#6EACDA]">Serial Number: </span>
                    {equipment.serialNumber}
                </p>
            </div>
            <ActionsButtons onEdit={onEdit} onDelete={onDelete} />
        </li>
    )
}